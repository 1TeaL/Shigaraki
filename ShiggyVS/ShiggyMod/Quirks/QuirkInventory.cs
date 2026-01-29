using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.Modules.Quirks
{
    /// <summary>
    /// Per-CharacterMaster quirk inventory.
    /// - MonoBehaviour (NOT NetworkBehaviour).
    /// - Server is authoritative: mutations happen on server.
    /// - Clients keep a mirrored copy via delta messages.
    /// - Client-only chat/toast is gated by "is this my local master?"
    /// </summary>
    public class QuirkInventory : MonoBehaviour
    {
        public CharacterMaster Master { get; private set; }

        private readonly HashSet<QuirkId> _owned = new HashSet<QuirkId>();
        public IReadOnlyCollection<QuirkId> Owned => _owned;

        private readonly List<QuirkId> _lastAutoCrafted = new List<QuirkId>();
        public IReadOnlyList<QuirkId> LastAutoCrafted => _lastAutoCrafted;

        public event Action OnOwnedChanged;

        // Useful if you want to gate UI logic on the local player
        public bool IsLocalPlayersMaster
        {
            get
            {
                if (!Master) return false;
                // Works for local client copies: local user's master matches this master
                var localUser = LocalUserManager.GetFirstLocalUser();
                var localMaster = localUser?.cachedMaster;
                return localMaster && localMaster == Master;
            }
        }

        // ------------------ Lifecycle ------------------

        private void Awake()
        {
            Master = GetComponent<CharacterMaster>();
        }

        // ------------------ Public API (read) ------------------

        public bool Has(QuirkId id) => id != QuirkId.None && _owned.Contains(id);

        // ------------------ Public API (client-facing) ------------------
        // These are safe to call from client code (input/UI/skills)

        /// <summary>
        /// Client-side request to add a quirk to THIS master (server authoritative).
        /// Call this when you "steal" or grant.
        /// </summary>
        public void RequestAddFromClient(QuirkId id)
        {
            if (id == QuirkId.None) return;
            if (!Master) return;

            // If we're already the server (host), mutate directly.
            if (NetworkServer.active)
            {
                Server_Add(id);
                return;
            }

            // Client -> Server request
            new Modules.Networking.QuirkInventoryAddRequest(Master.netId, (int)id)
                .Send(R2API.Networking.NetworkDestination.Server);
        }

        public void RequestAddFromClient(SkillDef sd)
        {
            if (sd == null) return;
            if (QuirkRegistry.QuirkLookup.TryFromSkill(sd, out var id))
                RequestAddFromClient(id);
        }

        public void RequestAddFromClient(BuffDef bd)
        {
            if (bd == null) return;
            if (QuirkRegistry.QuirkLookup.TryFromBuff(bd, out var id))
                RequestAddFromClient(id);
        }

        // ------------------ Server authoritative mutations ------------------

        /// <summary>
        /// Server-only: seeds this master's inventory at run start / spawn.
        /// Call this from server-side setup (e.g. ShiggyMasterController Run_Start).
        /// </summary>
        public void Server_SeedStartingQuirksFromConfig()
        {
            if (!NetworkServer.active) return;

            Server_Clear(silent: true);

            if (Modules.Config.StartWithAllQuirks != null && Modules.Config.StartWithAllQuirks.Value)
                Server_AddRange(QuirkRegistry.All.Keys);
            else
                Server_AddRange(GetShiggyBaseQuirks());
        }

        private void Server_AddRange(IEnumerable<QuirkId> ids)
        {
            if (!NetworkServer.active) return;

            bool changed = false;
            var added = new List<QuirkId>();

            foreach (var q in ids)
            {
                if (q == QuirkId.None) continue;
                if (_owned.Add(q))
                {
                    changed = true;
                    added.Add(q);
                }
            }

            if (TryAutoCraftAllFromOwnedServer(out var crafted))
            {
                changed = true;
                added.AddRange(crafted);
            }

            if (changed)
            {
                BroadcastDelta(added, craftedAuto: _lastAutoCrafted);
                OnOwnedChanged?.Invoke();
            }
        }

        private void Server_Clear(bool silent = false)
        {
            if (!NetworkServer.active) return;
            if (_owned.Count == 0) return;

            _owned.Clear();
            _lastAutoCrafted.Clear();

            if (!silent)
            {
                BroadcastDelta(added: Array.Empty<QuirkId>(), craftedAuto: Array.Empty<QuirkId>(), cleared: true);
                OnOwnedChanged?.Invoke();
            }
        }

        public bool Client_AddLocalOnly(QuirkId id)
        {
            if (id == QuirkId.None) return false;

            bool changed = _owned.Add(id);

            if (TryAutoCraftAllFromOwnedLocal(out var crafted))
                changed = true;

            if (changed)
                OnOwnedChanged?.Invoke();

            return changed;
        }

        private bool TryAutoCraftAllFromOwnedLocal(out List<QuirkId> craftedNow)
        {
            craftedNow = new List<QuirkId>();
            _lastAutoCrafted.Clear();

            bool anyChange = false;
            bool keepGoing;

            do
            {
                keepGoing = false;

                foreach (var kv in QuirkRegistry.All)
                {
                    var rec = kv.Value;
                    if (!rec.IsCrafted) continue;
                    if (_owned.Contains(rec.Id)) continue;

                    if (QuirkRegistry.CanCraft(_owned, rec.Id))
                    {
                        _owned.Add(rec.Id);
                        craftedNow.Add(rec.Id);
                        _lastAutoCrafted.Add(rec.Id);
                        keepGoing = true;
                        anyChange = true;
                    }
                }
            } while (keepGoing);

            return anyChange;
        }

        /// <summary>
        /// Server-only add. This is what the net message calls.
        /// </summary>
        public void Server_Add(QuirkId id)
        {
            if (!NetworkServer.active) return;
            if (id == QuirkId.None) return;

            bool changed = false;
            var added = new List<QuirkId>();
            var crafted = new List<QuirkId>();

            if (_owned.Add(id))
            {
                changed = true;
                added.Add(id);
            }

            if (TryAutoCraftAllFromOwnedServer(out var craftedNow))
            {
                changed = true;
                added.AddRange(craftedNow);
                crafted.AddRange(craftedNow);
            }

            if (changed)
            {
                BroadcastDelta(added, craftedAuto: crafted);
                OnOwnedChanged?.Invoke();
            }
        }

        // ------------------ Client apply deltas (from server broadcast) ------------------

        /// <summary>
        /// Called on clients when server broadcasts inventory changes for this master.
        /// </summary>
        public void Client_ApplyDelta(IReadOnlyList<QuirkId> added, IReadOnlyList<QuirkId> craftedAuto, bool cleared)
        {
            bool changed = false;

            if (cleared)
            {
                if (_owned.Count > 0)
                {
                    _owned.Clear();
                    changed = true;
                }
            }

            if (added != null)
            {
                foreach (var q in added)
                {
                    if (q == QuirkId.None) continue;
                    if (_owned.Add(q)) changed = true;
                }
            }

            _lastAutoCrafted.Clear();
            if (craftedAuto != null) _lastAutoCrafted.AddRange(craftedAuto);

            if (changed) OnOwnedChanged?.Invoke();

            // IMPORTANT: announcements only on owning client (not everyone)
            if (craftedAuto != null && craftedAuto.Count > 0 && IsLocalPlayersMaster)
            {
                foreach (var id in craftedAuto)
                {
                    var name = QuirkRegistry.GetDisplayName(id);
                    Chat.AddMessage($"Additionally, a new quirk combination has been made — {name}");
                }
            }
        }

        // ------------------ Server crafting ------------------

        private bool TryAutoCraftAllFromOwnedServer(out List<QuirkId> craftedNow)
        {
            craftedNow = new List<QuirkId>();
            _lastAutoCrafted.Clear();

            bool anyChange = false;
            bool keepGoing;

            do
            {
                keepGoing = false;

                foreach (var kv in QuirkRegistry.All)
                {
                    var rec = kv.Value;
                    if (!rec.IsCrafted) continue;
                    if (_owned.Contains(rec.Id)) continue;

                    if (QuirkRegistry.CanCraft(_owned, rec.Id))
                    {
                        _owned.Add(rec.Id);
                        craftedNow.Add(rec.Id);
                        _lastAutoCrafted.Add(rec.Id);
                        keepGoing = true;
                        anyChange = true;
                    }
                }
            } while (keepGoing);

            return anyChange;
        }

        private void BroadcastDelta(IReadOnlyList<QuirkId> added, IReadOnlyList<QuirkId> craftedAuto, bool cleared = false)
        {
            if (!NetworkServer.active) return;
            if (!Master) return;

            // Broadcast to ALL; clients will apply only if masterId matches.
            new Modules.Networking.QuirkInventoryDeltaMessage(
                    Master.netId,
                    added?.Select(x => (int)x).ToArray() ?? Array.Empty<int>(),
                    craftedAuto?.Select(x => (int)x).ToArray() ?? Array.Empty<int>(),
                    cleared
                )
                .Send(R2API.Networking.NetworkDestination.Clients);
        }

        private static List<QuirkId> GetShiggyBaseQuirks() => new List<QuirkId>
        {
            QuirkId.Shiggy_DecayActive,
            QuirkId.Shiggy_AirCannonActive,
            QuirkId.Shiggy_BulletLaserActive,
            QuirkId.Shiggy_MultiplierActive,
        };

        // ------------------ Convenience ------------------

        public static QuirkInventory Ensure(CharacterMaster master)
        {
            if (!master) return null;
            var inv = master.GetComponent<QuirkInventory>();
            if (!inv) inv = master.gameObject.AddComponent<QuirkInventory>();
            return inv;
        }

        // Keep your nested UI helper as-is
        public static class QuirkPickupUI
        {
            public static string BuildPickupText(QuirkId id)
            {
                var rec = QuirkRegistry.Get(id);
                string niceName = MakeNiceName(id);
                string type = rec.Category == QuirkCategory.Passive ? "Passive"
                           : rec.Category == QuirkCategory.Active ? "Active"
                           : "Utility";
                string style = rec.Category == QuirkCategory.Passive ? "cIsUtility" : "cIsDamage";
                return $"<style={style}>{niceName}</style> Quirk Get! ({type})";
            }

            public static string MakeNiceName(QuirkId id)
            {
                switch (id)
                {
                    case QuirkId.Elite_BlazingPassive: return "Blazing Quirk";
                    case QuirkId.Elite_GlacialPassive: return "Glacial Quirk";
                    case QuirkId.Elite_MalachitePassive: return "Malachite Quirk";
                    case QuirkId.Elite_CelestinePassive: return "Celestine Quirk";
                    case QuirkId.Elite_OverloadingPassive: return "Overloading Quirk";
                    case QuirkId.Elite_LunarPassive: return "Lunar Quirk";
                    case QuirkId.Elite_MendingPassive: return "Mending Quirk";
                    case QuirkId.Elite_VoidPassive: return "Void Quirk";
                    case QuirkId.Elite_GildedPassive: return "Gilded Quirk";
                    case QuirkId.Elite_TwistedPassive: return "Twisted Quirk";
                }
                return MakeNiceNameFromRaw(id.ToString());
            }

            private static string MakeNiceNameFromRaw(string raw)
            {
                int us = raw.LastIndexOf('_');
                string tail = us >= 0 ? raw.Substring(us + 1) : raw;
                tail = tail.Replace("Active", "").Replace("Passive", "");
                return string.IsNullOrWhiteSpace(tail) ? raw : tail;
            }
        }
    }
}
