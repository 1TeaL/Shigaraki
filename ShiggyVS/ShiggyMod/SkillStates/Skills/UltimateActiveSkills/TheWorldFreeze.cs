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
using System.ComponentModel;
using RoR2.CharacterAI;

namespace ShiggyMod.SkillStates
{
    public class TheWorldFreeze : BaseSkillState
    {
        Animator animator;
        BaseAI[] aiComponents;
        internal float previousAttackSpeedStat;

        public override void OnEnter()
        {
            base.OnEnter();

            //Disable master, baseai, character motor, animator

            animator = base.GetModelAnimator();
            if (animator)
            {
                animator.enabled = false;
            }
            previousAttackSpeedStat = base.attackSpeedStat;
            attackSpeedStat = 0f;

            if (base.characterDirection)
            {
                base.characterDirection.moveVector = base.characterDirection.forward;
            }
            if (base.characterMotor)
            {
                base.characterMotor.velocity = Vector3.zero;
                base.characterMotor.rootMotion = Vector3.zero;
                base.characterMotor.enabled = false;
            }
            else if (!base.characterMotor)
            {
                RigidbodyMotor rigidBodyMotor = base.gameObject.GetComponent<RigidbodyMotor>();
                rigidBodyMotor.moveVector = Vector3.zero;
                rigidBodyMotor.rootMotion = Vector3.zero;

                base.rigidbody.velocity = Vector3.zero;
                rigidBodyMotor.enabled = false;
            }
            if (characterBody.master)
            {
                characterBody.master.enabled = false;
            }

            aiComponents = characterBody.master.aiComponents;
            foreach (BaseAI aiComponent in aiComponents)
            {
                aiComponent.enabled = false;
            }


        }
        public override void OnExit()
        {
            base.OnExit();
            if (animator)
            {
                animator.enabled = true;
            }
            aiComponents = characterBody.master.aiComponents;
            foreach (BaseAI aiComponent in aiComponents)
            {
                aiComponent.enabled = true;
            }
            attackSpeedStat = previousAttackSpeedStat;

            if (base.characterMotor)
            {
                base.characterMotor.enabled = true;
            }

            RigidbodyMotor rigidBodyMotor = base.gameObject.GetComponent<RigidbodyMotor>();
            if (rigidBodyMotor)
            {
                rigidBodyMotor.enabled = true;
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();


            attackSpeedStat = 0f;


            if (base.characterDirection)
            {
                base.characterDirection.moveVector = base.characterDirection.forward;
            }
            if (base.characterMotor)
            {
                base.characterMotor.velocity = Vector3.zero;
                base.characterMotor.rootMotion = Vector3.zero;
            }
            else if (!base.characterMotor)
            {
                RigidbodyMotor rigidBodyMotor = base.gameObject.GetComponent<RigidbodyMotor>();
                rigidBodyMotor.moveVector = Vector3.zero;
                rigidBodyMotor.rootMotion = Vector3.zero;

                base.rigidbody.velocity = Vector3.zero;

            }


            if (!characterBody.HasBuff(Buffs.theWorldDebuff))
            {
                this.outer.SetNextStateToMain();
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Death;
        }

    }
}