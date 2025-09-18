using EntityStates;
using R2API.Networking;
using RoR2;
using ShiggyMod.Modules;
using ShiggyMod.Modules.Survivors;
using UnityEngine;

namespace ShiggyMod.SkillStates
{
    /// <summary>
    /// Runs under the "Flight" state machine so Weapon/Main are free for skills.
    /// - Hold Jump to ascend
    /// - Horizontal speed ~ ground moveSpeed
    /// - Sprint while flying: special pose + rotate body toward aim
    /// - Energy drains every 1.0s using user's formula
    /// </summary>
    public class AirWalk : BaseState
    {
        // Tunables
        private const float ascendSpeed = 12f;         // vertical speed while holding Jump
        private const float verticalDamp = 100f;         // how quickly we settle to 0 vertical when not ascending
        private const float aerialAccel = 100f;         // acceleration toward target horizontal velocity
        private const float maxBankAngle = 25f;        // optional visual banking (degrees) while sprinting

        private CharacterBody body;
        private CharacterMotor motor;
        private InputBankTest input;
        private CharacterDirection charDir;
        private ModelLocator modelLocator;
        private Animator animator;

        // Energy drain timer
        private float airwalkEnergyTimer;

        // Cached systems (replace with your actual types/fields)
        private EnergySystem energySystem; // <- your mod's energy component (assign below)


        public override void OnEnter()
        {
            base.OnEnter();

            body = characterBody;
            motor = characterMotor;
            input = base.inputBank;
            charDir = base.characterDirection;
            modelLocator = base.modelLocator;

            if (modelLocator && modelLocator.modelTransform)
                animator = modelLocator.modelTransform.GetComponent<Animator>();

            // Find your custom energy component however you normally do
            // e.g., energySystem = base.gameObject.GetComponent<EnergySystem>();
            energySystem = base.gameObject.GetComponent<EnergySystem>();

            // Disable gravity while flying
            if (motor)
            {
                motor.useGravity = false;
                motor.velocity = new Vector3(motor.velocity.x, 0f, motor.velocity.z);
                motor.Motor.RebuildCollidableLayers(); // good practice when changing states
            }

            // Animation flag
            if (animator) animator.SetBool("isFlying", true);

            // Optional SFX/VFX here
            // Util.PlaySound("Play_Shiggy_Flight_Start", gameObject);

            body.ApplyBuff(Buffs.airwalkBuff.buffIndex, 1);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!isAuthority) return; // this is a movement authority state

            if (!motor || !input || !body) return;

            // 1) Vertical control: hold Jump to go up; otherwise gently damp toward 0 vertical (hover)
            float vy = motor.velocity.y;
            if (input.jump.down)
            {
                vy = Mathf.MoveTowards(vy, ascendSpeed * body.moveSpeed, aerialAccel * Time.fixedDeltaTime);
            }
            else
            if (Input.GetKeyDown(Config.AFOHotkey.Value.MainKey))
            {
                vy = -Mathf.MoveTowards(vy, ascendSpeed * body.moveSpeed, aerialAccel * Time.fixedDeltaTime);
            }
            else
            {
                // ease vertical velocity toward 0 to "hover"
                vy = Mathf.MoveTowards(vy, 0f, verticalDamp * Time.fixedDeltaTime);
            }


            // --- Horizontal: input when walking, aim when sprinting ---
            bool sprint = body.isSprinting || input.sprint.down; // pick one; both shown for safety
            float sprintMult = sprint ? body.sprintingSpeedMultiplier : 1f;
            float targetSpeed = body.moveSpeed * sprintMult;

            // Decide desired direction
            Vector3 targetDir;
            if (sprint)
            {
                // Use aim direction, flattened to XZ
                Vector3 flatAim = Vector3.ProjectOnPlane(input.aimDirection, Vector3.up);
                targetDir = flatAim.sqrMagnitude > 0.0001f ? flatAim.normalized : Vector3.zero;
            }
            else
            {
                // Use move input (already camera-relative)
                Vector3 wish = input.moveVector;
                targetDir = wish.sqrMagnitude > 0.0001f ? wish.normalized : Vector3.zero;
            }

            // Smooth toward target velocity on XZ
            Vector3 targetHoriz = targetDir * targetSpeed;
            Vector3 currentHoriz = new Vector3(motor.velocity.x, 0f, motor.velocity.z);
            Vector3 newHoriz = Vector3.MoveTowards(currentHoriz, targetHoriz, aerialAccel * Time.fixedDeltaTime);

            // Commit velocity
            motor.velocity = new Vector3(newHoriz.x, vy, newHoriz.z);

            // Animator / sprint flag (optional)
            if (animator) animator.SetBool("isFlightSprint", sprint);

            // Face where we're going (sprint: face aim; else: face movement, fallback to aim)
            if (charDir)
            {
                Vector3 face =
                    sprint && targetDir.sqrMagnitude > 0.0001f
                    ? targetDir
                    : (newHoriz.sqrMagnitude > 0.01f
                        ? newHoriz.normalized
                        : Vector3.ProjectOnPlane(input.aimDirection, Vector3.up).normalized);

                if (face.sqrMagnitude > 0.0001f)
                    charDir.forward = face;
            }

            // 4) Energy drain every 1 second using your exact formula
            if (energySystem != null)
            {
                if (airwalkEnergyTimer <= 1f)
                {
                    airwalkEnergyTimer += Time.fixedDeltaTime;
                }
                else
                {
                    // Your provided cost block (unchanged)
                    float plusChaosflatCost = StaticValues.airwalkEnergyFraction * energySystem.maxPlusChaos - (energySystem.costflatplusChaos);
                    if (plusChaosflatCost < 0f) plusChaosflatCost = StaticValues.minimumCostFlatPlusChaosSpend;

                    float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
                    if (plusChaosCost < 0f) plusChaosCost = 0f;

                    energySystem.SpendplusChaos(plusChaosCost);
                    airwalkEnergyTimer = 0f;

                    // OPTIONAL auto-fallout if no energy
                    if (energySystem.currentplusChaos < 1f) // replace with your real check
                    {
                        outer.SetNextState(new Idle());
                        return;
                    }
                }
            }
        }

        public override void OnExit()
        {
            // Restore gravity
            if (motor) motor.useGravity = true;

            body.ApplyBuff(Buffs.airwalkBuff.buffIndex, 0);
            // Clear animator flags
            if (animator)
            {
                animator.SetBool("isFlying", false);
                animator.SetBool("isFlightSprint", false);
            }

            // Optional SFX/VFX end

            base.OnExit();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            // Allow skills/damage to interrupt if needed; movement-only layer should be low priority.
            return InterruptPriority.Any;
        }
    }

    /// <summary>
    /// No-op idle for the Flight machine
    /// </summary>
    public class Idle : BaseState
    {
        public override void OnEnter() { base.OnEnter(); }
        public override void FixedUpdate() { base.FixedUpdate(); }
        public override InterruptPriority GetMinimumInterruptPriority() => InterruptPriority.Any;
    }
}
