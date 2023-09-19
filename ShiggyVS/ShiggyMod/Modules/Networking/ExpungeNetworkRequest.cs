using R2API;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.UI.Image;


namespace ShiggyMod.Modules.Networking
{
    internal class ExpungeNetworkRequest : INetMessage
    {
        //Network these ones.
        NetworkInstanceId charnetID;
        Vector3 direction;
        private float range;
        private float damage;

        //Don't network these.
        GameObject bodyObj;
        GameObject charbodyObj;
        private BullseyeSearch search;
        private List<HurtBox> trackingTargets;
        private GameObject blastEffectPrefab = Assets.impBossExplosionEffect;

        public ExpungeNetworkRequest()
        {

        }

        public ExpungeNetworkRequest(NetworkInstanceId charnetID, Vector3 direction, float range, float damage)
        {
            this.direction = direction;
            this.range = range;
            this.damage = damage;
            this.charnetID = charnetID;
        }

        public void Deserialize(NetworkReader reader)
        {
            charnetID = reader.ReadNetworkId();
            direction = reader.ReadVector3();
            range = reader.ReadSingle();
            damage = reader.ReadSingle();
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(charnetID);
            writer.Write(direction);
            writer.Write(range);
            writer.Write(damage);
        }

        public void OnReceived()
        {

            if (NetworkServer.active)
            {
                search = new BullseyeSearch();

                GameObject charmasterobject = Util.FindNetworkObject(charnetID);
                CharacterMaster charcharMaster = charmasterobject.GetComponent<CharacterMaster>();
                CharacterBody charcharBody = charcharMaster.GetBody();
                charbodyObj = charcharBody.gameObject;

                //Damage target and stun
                SearchForTarget(charcharBody);
                DamageTargets(charcharBody);
            }
        }

        private void SearchForTarget(CharacterBody charBody)
        {
            this.search.teamMaskFilter = TeamMask.GetUnprotectedTeams(charBody.teamComponent.teamIndex);
            this.search.filterByLoS = true;
            this.search.searchOrigin = charBody.corePosition;
            this.search.searchDirection = direction;
            this.search.sortMode = BullseyeSearch.SortMode.Distance;
            this.search.maxDistanceFilter = range;
            this.search.maxAngleFilter = 360f;
            this.search.RefreshCandidates();
            this.search.FilterOutGameObject(charBody.gameObject);
            this.trackingTargets = this.search.GetResults().ToList<HurtBox>();
        }

        private void DamageTargets(CharacterBody charcharBody)
        {
            if (trackingTargets.Count > 0)
            {
                foreach (HurtBox singularTarget in trackingTargets)
                {
                    CharacterBody victimBody = singularTarget.healthComponent.body;

                    //deal bonus damage based on number of debuffs
                    int debuffCount = 0;
                    DotController d = DotController.FindDotController(singularTarget.gameObject);

                    foreach (BuffIndex buffType in BuffCatalog.debuffBuffIndices)
                    {
                        if (victimBody.HasBuff(buffType))
                        {
                            debuffCount++;
                        }
                    }

                    DotController dotController = DotController.FindDotController(singularTarget.gameObject);
                    if (dotController)
                    {
                        for (DotController.DotIndex dotIndex = DotController.DotIndex.Bleed; dotIndex < DotController.DotIndex.Count; dotIndex++)
                        {
                            if (dotController.HasDotActive(dotIndex))
                            {
                                if (victimBody.HasBuff(RoR2Content.Buffs.Bleeding))
                                {
                                    debuffCount += victimBody.GetBuffCount(RoR2Content.Buffs.Bleeding) - 1;
                                }
                                if (victimBody.HasBuff(RoR2Content.Buffs.SuperBleed))
                                {
                                    debuffCount += victimBody.GetBuffCount(RoR2Content.Buffs.SuperBleed) - 1;
                                }
                                if (victimBody.HasBuff(RoR2Content.Buffs.OnFire))
                                {
                                    debuffCount += victimBody.GetBuffCount(RoR2Content.Buffs.OnFire) - 1;
                                }
                                if (victimBody.HasBuff(RoR2Content.Buffs.Blight))
                                {
                                    debuffCount += victimBody.GetBuffCount(RoR2Content.Buffs.Blight) - 1;
                                }
                                debuffCount++;
                            }
                        }
                    }

                    if (victimBody.HasBuff(Buffs.decayDebuff))
                    {
                        int decayStacks = victimBody.GetBuffCount(Buffs.decayDebuff);
                        debuffCount += decayStacks;
                    }

                    //Debug.Log(debuffCount + "debuffcount");
                    float buffDamage = 0f;
                    float buffBaseDamage = charcharBody.damage * StaticValues.expungeDamageMultiplier;
                    buffDamage = buffBaseDamage * debuffCount;                


                    DamageInfo damageInfo = new DamageInfo
                    {
                        attacker = charbodyObj,
                        inflictor = charbodyObj,
                        damage = damage += buffDamage,
                        position = singularTarget.transform.position,
                        procCoefficient = StaticValues.expungeProcCoefficient,
                        damageType = DamageType.BypassArmor,
                        crit = charcharBody.RollCrit(),

                    };

                    singularTarget.healthComponent.TakeDamage(damageInfo);
                    GlobalEventManager.instance.OnHitEnemy(damageInfo, singularTarget.healthComponent.gameObject);


                    EffectManager.SpawnEffect(blastEffectPrefab, new EffectData
                    {
                        origin = singularTarget.transform.position,
                        scale = 1f,
                        rotation = Quaternion.LookRotation(direction),

                    }, true);

                }
            }

            
        }      

    }
}