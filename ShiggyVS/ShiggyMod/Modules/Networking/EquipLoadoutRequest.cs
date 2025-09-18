using ExtraSkillSlots;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Skills;
using ShiggyMod.Modules.Quirks;
using ShiggyMod.Modules.Survivors;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ShiggyMod.Modules.Networking
{
    /// <summary>
    /// Client -> Server: equip a full Shiggy loadout.
    /// Server will:
    ///  • If body exists now: QuirkEquip.ApplyServer(body, extras, loadout) (this persists 0..7 internally).
    ///  • If body does not exist: resolve SkillDefs and persist via writeToSkillList(def, index) for 0..7.
    /// </summary>
    internal class EquipLoadoutRequest : INetMessage
    {
        private NetworkInstanceId masterId;
        private SelectedQuirkLoadoutNet netLoadout;

        public EquipLoadoutRequest() { }
        public EquipLoadoutRequest(NetworkInstanceId masterId, SelectedQuirkLoadout loadout)
        {
            this.masterId = masterId;
            this.netLoadout = SelectedQuirkLoadoutNet.From(loadout);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(masterId);
            netLoadout.Serialize(writer);
        }

        public void Deserialize(NetworkReader reader)
        {
            masterId = reader.ReadNetworkId();
            netLoadout = SelectedQuirkLoadoutNet.Deserialize(reader);
        }

        public void OnReceived()
        {
            if (!NetworkServer.active) return;

            var masterGO = Util.FindNetworkObject(masterId);
            if (!masterGO) return;

            var master = masterGO.GetComponent<CharacterMaster>();
            if (!master) return;

            var smc = masterGO.GetComponent<ShiggyMasterController>();
            if (!smc) return;

            var loadout = netLoadout.ToRuntime();
            var body = master.GetBody();
            if (body)
            {
                // Server-authoritative apply; this ALSO persists 0..7 internally.
                var extras = body.GetComponent<ExtraSkillLocator>();
                QuirkEquip.ApplyServer(body, extras, loadout);
            }
            else
            {
                // No body yet — just persist 0..7 so CharacterBody.Start will reapply later.
                var defs = ResolveSkillDefs(loadout);
                for (int i = 0; i < 8; i++)
                    smc.writeToSkillList(defs[i], i);
            }
        }

        // ---- Helpers ----

        private static SkillDef[] ResolveSkillDefs(SelectedQuirkLoadout loadout)
        {
            SkillDef SD(QuirkId q)
            {
                if (QuirkRegistry.TryGet(q, out var rec) && rec.Skill) return rec.Skill;
                // Fallbacks for base four
                switch (q)
                {
                    case QuirkId.Shiggy_DecayActive: return Survivors.Shiggy.decayDef;
                    case QuirkId.Shiggy_AirCannonActive: return Survivors.Shiggy.aircannonDef;
                    case QuirkId.Shiggy_BulletLaserActive: return Survivors.Shiggy.bulletlaserDef;
                    case QuirkId.Shiggy_MultiplierActive: return Survivors.Shiggy.multiplierDef;
                    default: return null;
                }
            }

            return new[]
            {
                SD(loadout.Primary),
                SD(loadout.Secondary),
                SD(loadout.Utility),
                SD(loadout.Special),
                SD(loadout.Extra1),
                SD(loadout.Extra2),
                SD(loadout.Extra3),
                SD(loadout.Extra4),
            };
        }

        // Compact, network-serializable copy of SelectedQuirkLoadout
        private struct SelectedQuirkLoadoutNet
        {
            public int Primary, Secondary, Utility, Special;
            public int Extra1, Extra2, Extra3, Extra4;
            public List<int> PassiveToggles; // optional

            public static SelectedQuirkLoadoutNet From(SelectedQuirkLoadout src)
            {
                return new SelectedQuirkLoadoutNet
                {
                    Primary = (int)src.Primary,
                    Secondary = (int)src.Secondary,
                    Utility = (int)src.Utility,
                    Special = (int)src.Special,
                    Extra1 = (int)src.Extra1,
                    Extra2 = (int)src.Extra2,
                    Extra3 = (int)src.Extra3,
                    Extra4 = (int)src.Extra4,
                    PassiveToggles = src.PassiveToggles != null
                        ? new List<int>(src.PassiveToggles.ConvertAll(p => (int)p))
                        : new List<int>(0)
                };
            }

            public void Serialize(NetworkWriter w)
            {
                w.Write(Primary); w.Write(Secondary);
                w.Write(Utility); w.Write(Special);
                w.Write(Extra1); w.Write(Extra2);
                w.Write(Extra3); w.Write(Extra4);

                ushort count = (ushort)(PassiveToggles?.Count ?? 0);
                w.Write(count);
                for (int i = 0; i < count; i++) w.Write(PassiveToggles[i]);
            }

            public static SelectedQuirkLoadoutNet Deserialize(NetworkReader r)
            {
                var n = new SelectedQuirkLoadoutNet
                {
                    Primary = r.ReadInt32(),
                    Secondary = r.ReadInt32(),
                    Utility = r.ReadInt32(),
                    Special = r.ReadInt32(),
                    Extra1 = r.ReadInt32(),
                    Extra2 = r.ReadInt32(),
                    Extra3 = r.ReadInt32(),
                    Extra4 = r.ReadInt32(),
                };

                int count = r.ReadUInt16();
                n.PassiveToggles = new List<int>(count);
                for (int i = 0; i < count; i++) n.PassiveToggles.Add(r.ReadInt32());
                return n;
            }

            public SelectedQuirkLoadout ToRuntime()
            {
                var rt = new SelectedQuirkLoadout
                {
                    Primary = (QuirkId)Primary,
                    Secondary = (QuirkId)Secondary,
                    Utility = (QuirkId)Utility,
                    Special = (QuirkId)Special,
                    Extra1 = (QuirkId)Extra1,
                    Extra2 = (QuirkId)Extra2,
                    Extra3 = (QuirkId)Extra3,
                    Extra4 = (QuirkId)Extra4,
                    PassiveToggles = new List<QuirkId>(PassiveToggles?.Count ?? 0)
                };

                if (PassiveToggles != null)
                    for (int i = 0; i < PassiveToggles.Count; i++)
                        rt.PassiveToggles.Add((QuirkId)PassiveToggles[i]);

                return rt;
            }
        }
    }
}
