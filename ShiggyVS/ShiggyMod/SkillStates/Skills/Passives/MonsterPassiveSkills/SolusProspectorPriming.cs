using EntityStates;
using EntityStates.JellyfishMonster;
using HG;
using R2API.Networking;
using R2API.Networking.Interfaces;
using RoR2;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Networking;
using ShiggyMod.Modules.Survivors;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.ParticleSystem.PlaybackState;

namespace ShiggyMod.SkillStates
{
    public class SolusProspectorPriming : Skill
    {

        public override void OnEnter()
        {
            base.OnEnter();
            if (NetworkServer.active)
            {
                characterBody.ApplyBuff(Modules.Buffs.solusPrimedBuff.buffIndex, 1);
            }
        }

    }
}
