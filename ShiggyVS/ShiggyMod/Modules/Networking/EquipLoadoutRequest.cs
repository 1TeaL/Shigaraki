// EquipLoadoutRequest.cs
using ExtraSkillSlots;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using RoR2.Skills;
using ShiggyMod.Modules.Quirks;
using ShiggyMod.Modules.Survivors;
using UnityEngine.Networking;

namespace ShiggyMod.Modules.Networking
{
    internal class EquipLoadoutRequest : INetMessage
    {
        private NetworkInstanceId masterId;
        private NetworkInstanceId requesterNetUserId;   // NEW
        private SelectedQuirkLoadoutNet netLoadout;

        public EquipLoadoutRequest() { }

        public EquipLoadoutRequest(NetworkInstanceId masterId, SelectedQuirkLoadout loadout, NetworkInstanceId requesterNetUserId)
        {
            this.masterId = masterId;
            this.netLoadout = SelectedQuirkLoadoutNet.From(loadout);
            this.requesterNetUserId = requesterNetUserId;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(masterId);
            writer.Write(requesterNetUserId); // NEW
            netLoadout.Serialize(writer);
        }

        public void Deserialize(NetworkReader reader)
        {
            masterId = reader.ReadNetworkId();
            requesterNetUserId = reader.ReadNetworkId(); // NEW
            netLoadout = SelectedQuirkLoadoutNet.Deserialize(reader);
        }

        public void OnReceived()
        {
            if (!NetworkServer.active) return;

            var masterGO = Util.FindNetworkObject(masterId);
            if (!masterGO) return;

            var master = masterGO.GetComponent<CharacterMaster>();
            if (!master) return;

            var loadout = netLoadout.ToRuntime();
            var body = master.GetBody();

            if (body)
            {
                var extras = body.GetComponent<ExtraSkillLocator>();
                QuirkEquip.ApplyServer(body, extras, loadout);

                // NEW: respond to requesting client so they apply locally
                SendResultToRequester(loadout);

                return;
            }

            // ---- Persistence path only ----
            var smc = masterGO.GetComponent<ShiggyMasterController>();
            if (!smc) return;

            var defs = ResolveSkillDefs(loadout);
            for (int i = 0; i < 8; i++)
                smc.writeToSkillList(defs[i], i);

            // NEW: even if no body right now, still send result so UI client updates instantly when body exists
            SendResultToRequester(loadout);
        }

        private void SendResultToRequester(SelectedQuirkLoadout loadout)
        {
            var nuGO = Util.FindNetworkObject(requesterNetUserId);
            if (!nuGO) return;

            var nu = nuGO.GetComponent<NetworkUser>();
            if (!nu) return;

            var conn = nu.connectionToClient;
            if (conn == null) return;

            new EquipLoadoutResult(masterId, loadout).Send(conn);
        }

        // ---------------- Helpers ----------------

        private static SkillDef[] ResolveSkillDefs(SelectedQuirkLoadout loadout)
        {
            SkillDef SD(QuirkId q)
            {
                if (q == QuirkId.None) return null;

                if (QuirkRegistry.TryGet(q, out var rec) && rec.SkillDef != null)
                    return rec.SkillDef;

                switch (q)
                {
                    case QuirkId.Shiggy_DecayActive: return Shiggy.decayDef;
                    case QuirkId.Shiggy_AirCannonActive: return Shiggy.aircannonDef;
                    case QuirkId.Shiggy_BulletLaserActive: return Shiggy.bulletlaserDef;
                    case QuirkId.Shiggy_MultiplierActive: return Shiggy.multiplierDef;
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

        // NOTE: made public so EquipLoadoutResult can reuse it without duplication.
        internal struct SelectedQuirkLoadoutNet
        {
            public int Primary, Secondary, Utility, Special;
            public int Extra1, Extra2, Extra3, Extra4;

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
                };
            }

            public void Serialize(NetworkWriter w)
            {
                w.Write(Primary); w.Write(Secondary);
                w.Write(Utility); w.Write(Special);
                w.Write(Extra1); w.Write(Extra2);
                w.Write(Extra3); w.Write(Extra4);
            }

            public static SelectedQuirkLoadoutNet Deserialize(NetworkReader r)
            {
                return new SelectedQuirkLoadoutNet
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
            }

            public SelectedQuirkLoadout ToRuntime()
            {
                return new SelectedQuirkLoadout
                {
                    Primary = (QuirkId)Primary,
                    Secondary = (QuirkId)Secondary,
                    Utility = (QuirkId)Utility,
                    Special = (QuirkId)Special,
                    Extra1 = (QuirkId)Extra1,
                    Extra2 = (QuirkId)Extra2,
                    Extra3 = (QuirkId)Extra3,
                    Extra4 = (QuirkId)Extra4,
                };
            }
        }
    }
}
