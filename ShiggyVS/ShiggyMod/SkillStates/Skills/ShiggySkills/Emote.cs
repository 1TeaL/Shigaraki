using EntityStates;
using RoR2;
using ShiggyMod.Modules;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    public class Emote : BaseSkillState
    {
        private Animator animator;
        private int emoteLayerIndex = -1;
        private float previousEmoteLayerWeight = 0f;

        public override void OnEnter()
        {
            base.OnEnter();

            animator = GetModelAnimator();
            if (!animator) return;

            emoteLayerIndex = animator.GetLayerIndex("Emote, Override");
            if (emoteLayerIndex >= 0)
            {
                previousEmoteLayerWeight = animator.GetLayerWeight(emoteLayerIndex);
                animator.SetLayerWeight(emoteLayerIndex, 1f);
            }

            animator.SetBool("emote", true);

            // Play emote on its own layer
            PlayCrossfade("Emote, Override", "LayDown", "Attack.playbackRate", 1f, 0.05f);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            // Keep emote layer dominant (some states mess with weights)
            if (animator && emoteLayerIndex >= 0)
                animator.SetLayerWeight(emoteLayerIndex, 1f);
        }

        public override void OnExit()
        {
            if (animator)
            {
                animator.SetBool("emote", false);

                // IMPORTANT: you need an "Empty" (or similar) state on this layer
                // that doesn't override the body. Crossfade to it so LayDown stops.
                PlayCrossfade("Emote, Override", "Idle", 0.05f);

                if (emoteLayerIndex >= 0)
                    animator.SetLayerWeight(emoteLayerIndex, previousEmoteLayerWeight);

                animator.SetLayerWeight(emoteLayerIndex, 0f);
            }

            base.OnExit();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            // Frozen means *hard to interrupt*; fine if you want emote to persist.
            // But it also means other systems won't kick you out.
            return InterruptPriority.Frozen;
        }
    }
}
