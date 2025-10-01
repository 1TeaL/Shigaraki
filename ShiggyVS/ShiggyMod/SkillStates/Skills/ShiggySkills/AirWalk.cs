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
        private float aerialAccel;         // acceleration toward target horizontal velocity
        private const float maxBankAngle = 25f;        // optional visual banking (degrees) while sprinting

        private CharacterBody body;
        private CharacterMotor motor;
        private InputBankTest input;
        private CharacterDirection charDir;
        private ModelLocator modelLocator;
        private Animator animator;
        private Vector3 lastPlanarFacing = Vector3.forward; // helps avoid jitter when inputs are tiny


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
            aerialAccel = Mathf.Max(100f, body.acceleration * 2f); 
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!isAuthority) return;
            if (!motor || !input || !body) return;

            
            // --- Common speed targets ---
            bool sprintInput = body.isSprinting || input.sprint.down;
            float sprintMult = sprintInput ? body.sprintingSpeedMultiplier : 1f;
            float targetSpeed = body.moveSpeed * sprintMult;

            Vector3 velocity = motor.velocity;

            // ===== BRANCH A: SPRINT = full 3D flight along aim (includes Y) =====
            if (sprintInput)
            {
                Vector3 aimDir = input.aimDirection;
                if (aimDir.sqrMagnitude > 0.0001f)
                {
                    aimDir.Normalize();

                    // Move whole velocity toward aim * targetSpeed (true 3D sprint flight)
                    Vector3 targetVel = aimDir * targetSpeed;
                    velocity = Vector3.MoveTowards(velocity, targetVel, aerialAccel * Time.fixedDeltaTime);

                    // Animator sprint flag
                    if (animator) animator.SetBool("isFlightSprint", true);

                    // Face planar component of the aim to avoid pitch jitter on CharacterDirection
                    Vector3 flatAim = Vector3.ProjectOnPlane(aimDir, Vector3.up);
                    if (flatAim.sqrMagnitude > 0.0001f)
                        lastPlanarFacing = flatAim.normalized;
                }
            }
            // ===== BRANCH B: NOT SPRINTING = hover controls (Jump up / descend / damp to 0) =====
            else
            {
                if (animator) animator.SetBool("isFlightSprint", false);
                bool descendHeld = Input.GetKeyDown(Config.AFOHotkey.Value.MainKey);
                // Vertical
                float vy = velocity.y;
                if (input.jump.down)
                {
                    // Upward climb (use fixed ascendSpeed; not scaled by moveSpeed to keep it predictable)
                    vy = Mathf.MoveTowards(vy, ascendSpeed, aerialAccel * Time.fixedDeltaTime);
                }
                else if (descendHeld) // your descend key
                {
                    //vy = Mathf.MoveTowards(vy, -ascendSpeed, aerialAccel * Time.fixedDeltaTime);
                    vy = -ascendSpeed;
                }
                else
                {
                    // Hover damping toward 0 only when NOT sprinting
                    vy = Mathf.MoveTowards(vy, 0f, verticalDamp * Time.fixedDeltaTime);
                }

                // Horizontal: camera-relative move input at ground speed
                Vector3 wish = input.moveVector; // already camera-relative
                Vector3 targetHoriz = Vector3.zero;
                if (wish.sqrMagnitude > 0.0001f)
                {
                    wish.Normalize();
                    targetHoriz = wish * targetSpeed;
                    lastPlanarFacing = wish; // remember for facing
                }

                Vector3 currentHoriz = new Vector3(velocity.x, 0f, velocity.z);
                Vector3 newHoriz = Vector3.MoveTowards(currentHoriz, targetHoriz, aerialAccel * Time.fixedDeltaTime);

                velocity = new Vector3(newHoriz.x, vy, newHoriz.z);
            }

            // Commit velocity
            motor.velocity = velocity;

            // --- Character facing: always planar to avoid look jitter ---
            if (charDir)
            {
                Vector3 planarFace = lastPlanarFacing;
                if (planarFace.sqrMagnitude < 0.0001f)
                {
                    // Fallback to planar aim
                    Vector3 flatAim = Vector3.ProjectOnPlane(input.aimDirection, Vector3.up);
                    planarFace = flatAim.sqrMagnitude > 0.0001f ? flatAim.normalized : charDir.forward;
                }
                charDir.forward = planarFace;
            }

            // --- Energy drain, unchanged from your code ---
            if (energySystem != null)
            {
                if (airwalkEnergyTimer <= 1f)
                {
                    airwalkEnergyTimer += Time.fixedDeltaTime;
                }
                else
                {
                    float plusChaosflatCost = StaticValues.airwalkEnergyFraction * energySystem.maxPlusChaos - (energySystem.costflatplusChaos);
                    if (plusChaosflatCost < 0f) plusChaosflatCost = StaticValues.minimumCostFlatPlusChaosSpend;

                    float plusChaosCost = energySystem.costmultiplierplusChaos * plusChaosflatCost;
                    if (plusChaosCost < 0f) plusChaosCost = 0f;

                    energySystem.SpendplusChaos(plusChaosCost);
                    airwalkEnergyTimer = 0f;

                    if (energySystem.currentplusChaos < 1f)
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
