﻿using ShiggyMod.Modules.Survivors;
using EntityStates;
using RoR2;
using UnityEngine;
using System.Collections.Generic;
using ShiggyMod.Modules;
using UnityEngine.Networking;
using RoR2.ExpansionManagement;
using ExtraSkillSlots;
using R2API.Networking;
using System;
using static UnityEngine.UI.Image;
using R2API;

namespace ShiggyMod.SkillStates
{
    public class FinalReleaseMugetsu : BaseSkillState
    {
        public ShiggyController Shiggycon;
        private float finalReleaseMugetsuTimer = 0f;
        private int currentReleaseCount = 0;

        public override void OnEnter()
        {
            base.OnEnter();
            //play animation and maybe particles?

            base.GetModelAnimator().SetFloat("Attack.playbackRate", attackSpeedStat);
            Shiggycon = gameObject.GetComponent<ShiggyController>();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            //do mugetsu if no energy- need to put this in the skill as well
            int finalReleaseCount = characterBody.GetBuffCount(Buffs.finalReleaseBuff);

            if (finalReleaseMugetsuTimer >= 0f)
            {
                finalReleaseMugetsuTimer -= Time.fixedDeltaTime * Shiggycon.OFAFOTimeMultiplier * characterBody.attackSpeed;
            }
            //repeat a blast, increment the count by 10 each time.
            else if (finalReleaseMugetsuTimer < 0f)
            {
                if (currentReleaseCount < finalReleaseCount)
                {
                    BlastAttack blastAttack = new BlastAttack();
                    blastAttack.radius = StaticValues.finalReleaseBaseRadius + StaticValues.finalReleaseRadiusPerStackCoefficient * currentReleaseCount;
                    blastAttack.procCoefficient = StaticValues.finalReleaseProcCoefficient * currentReleaseCount;
                    blastAttack.position = characterBody.corePosition;
                    blastAttack.attacker = characterBody.gameObject;
                    blastAttack.crit = characterBody.RollCrit();
                    blastAttack.baseDamage = characterBody.damage * StaticValues.finalReleaseDamageCoefficient * currentReleaseCount;
                    blastAttack.falloffModel = BlastAttack.FalloffModel.None;
                    blastAttack.baseForce = StaticValues.finalReleaseForceCoefficient * currentReleaseCount;
                    blastAttack.teamIndex = characterBody.teamComponent.teamIndex;
                    blastAttack.damageType = DamageType.Stun1s;
                    blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
                    blastAttack.AddModdedDamageType(Modules.Damage.shiggyDecay);
                    blastAttack.Fire();

                    EffectManager.SpawnEffect(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/effects/SonicBoomEffect"), new EffectData
                    {
                        origin = characterBody.corePosition,
                        scale = StaticValues.finalReleaseBaseRadius + StaticValues.finalReleaseRadiusPerStackCoefficient * currentReleaseCount,
                    }, true);
                    EffectManager.SpawnEffect(EntityStates.Engi.SpiderMine.Detonate.blastEffectPrefab, new EffectData
                    {
                        origin = characterBody.corePosition,
                        scale = StaticValues.finalReleaseBaseRadius + StaticValues.finalReleaseRadiusPerStackCoefficient * currentReleaseCount,
                    }, true);
                    EffectManager.SpawnEffect(Assets.mercOmnimpactVFXPrefab, new EffectData
                    {
                        origin = characterBody.corePosition,
                        scale = StaticValues.finalReleaseBaseRadius + StaticValues.finalReleaseRadiusPerStackCoefficient * currentReleaseCount,
                    }, true);
                    EffectManager.SpawnEffect(Assets.mercOmnimpactVFXEvisPrefab, new EffectData
                    {
                        origin = characterBody.corePosition,
                        scale = StaticValues.finalReleaseBaseRadius + StaticValues.finalReleaseRadiusPerStackCoefficient * currentReleaseCount,
                    }, true);

                    finalReleaseMugetsuTimer += StaticValues.finalReleaseMugetsuInterval;
                    currentReleaseCount += StaticValues.finalReleaseCountIncrement;
                }
                else if (currentReleaseCount >= finalReleaseCount)
                {

                    BlastAttack blastAttack = new BlastAttack();
                    blastAttack.radius = StaticValues.finalReleaseBaseRadius + StaticValues.finalReleaseRadiusPerStackCoefficient * currentReleaseCount;
                    blastAttack.procCoefficient = StaticValues.finalReleaseProcCoefficient * currentReleaseCount;
                    blastAttack.position = characterBody.corePosition;
                    blastAttack.attacker = characterBody.gameObject;
                    blastAttack.crit = characterBody.RollCrit();
                    blastAttack.baseDamage = characterBody.damage * StaticValues.finalReleaseDamageCoefficient * currentReleaseCount;
                    blastAttack.falloffModel = BlastAttack.FalloffModel.None;
                    blastAttack.baseForce = StaticValues.finalReleaseForceCoefficient * currentReleaseCount;
                    blastAttack.teamIndex = characterBody.teamComponent.teamIndex;
                    blastAttack.damageType = DamageType.Stun1s;
                    blastAttack.attackerFiltering = AttackerFiltering.NeverHitSelf;
                    blastAttack.AddModdedDamageType(Modules.Damage.shiggyDecay);
                    blastAttack.Fire();

                    EffectManager.SpawnEffect(Assets.mercOmnimpactVFXPrefab, new EffectData
                    {
                        origin = characterBody.corePosition,
                        scale = StaticValues.finalReleaseBaseRadius + StaticValues.finalReleaseRadiusPerStackCoefficient * currentReleaseCount,
                    }, true);
                    EffectManager.SpawnEffect(Assets.mercOmnimpactVFXEvisPrefab, new EffectData
                    {
                        origin = characterBody.corePosition,
                        scale = StaticValues.finalReleaseBaseRadius + StaticValues.finalReleaseRadiusPerStackCoefficient * currentReleaseCount,
                    }, true);
                    EffectManager.SpawnEffect(EntityStates.Merc.GroundLight.comboSwingEffectPrefab, new EffectData
                    {
                        origin = characterBody.corePosition,
                        scale = StaticValues.finalReleaseBaseRadius + StaticValues.finalReleaseRadiusPerStackCoefficient * currentReleaseCount,
                    }, true);
                    EffectManager.SpawnEffect(EntityStates.Merc.GroundLight.finisherHitEffectPrefab, new EffectData
                    {
                        origin = characterBody.corePosition,
                        scale = StaticValues.finalReleaseBaseRadius + StaticValues.finalReleaseRadiusPerStackCoefficient * currentReleaseCount,
                    }, true);
                    EffectManager.SpawnEffect(EntityStates.VagrantMonster.FireMegaNova.novaEffectPrefab, new EffectData
                    {
                        origin = characterBody.corePosition,
                        scale = StaticValues.finalReleaseBaseRadius + StaticValues.finalReleaseRadiusPerStackCoefficient * currentReleaseCount,
                    }, true);


                    currentReleaseCount = 0;
                    characterBody.ApplyBuff(Buffs.finalReleaseBuff.buffIndex, 0);
                    this.outer.SetNextStateToMain();
                    return;

                }
            }
            
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }
    }
}