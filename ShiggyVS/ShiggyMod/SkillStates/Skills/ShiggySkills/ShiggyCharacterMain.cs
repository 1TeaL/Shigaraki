using EntityStates;
using RoR2;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    // Inherits the default locomotion/skill processing but adds a "Flight" machine toggle via double-tap Jump.
    internal class ShiggyCharacterMain : GenericCharacterMain
    {
        private EntityStateMachine weaponStateMachine;
        private EntityStateMachine flightStateMachine;

        // double-tap detection
        private float lastJumpTapTime;
        private const float doubleTapWindow = 0.3f;

        public override void OnEnter()
        {
            base.OnEnter();

            // Weapon machine (optional, for checks)
            weaponStateMachine = EntityStateMachine.FindByCustomName(base.gameObject, "Weapon");

            // Flight machine (custom)
            flightStateMachine = EntityStateMachine.FindByCustomName(base.gameObject, "Flight");

        }

        private bool IsInFlight()
        {
            return flightStateMachine && flightStateMachine.state is AirWalk;
        }

        public override void ProcessJump()
        {
            // Perform vanilla jump first so you don't break normal ground jumps
            base.ProcessJump();

            if (!hasInputBank || !isAuthority || !inputBank) return;

            if (inputBank.jump.justPressed)
            {
                float now = Time.time;
                if (now - lastJumpTapTime <= doubleTapWindow)
                {
                    // Double-tap detected
                    if (IsInFlight())
                    {
                        // Exit flight
                        flightStateMachine.SetNextState(new Idle());
                    }
                    else
                    {
                        // Enter flight
                        flightStateMachine.SetNextState(new AirWalk());
                    }
                    lastJumpTapTime = 0f; // reset
                }
                else
                {
                    lastJumpTapTime = now;
                }
            }
        }

        public override void OnExit()
        {
            // If you want to force-exit flight whenever CharacterMain exits:
            // if (isAuthority && flightStateMachine) flightStateMachine.SetNextState(new Idle());
            base.OnExit();
        }
    }
}
