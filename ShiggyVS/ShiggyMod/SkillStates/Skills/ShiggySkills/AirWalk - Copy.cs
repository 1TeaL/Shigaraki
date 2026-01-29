namespace ShiggyMod.SkillStates
{
    ////unused 
    //public class Gliding : BaseState
    //{
    //    private ShiggyController shiggyCon;
    //    private Animator anim;
    //    private static float slowestDescent = -3f;
    //    private bool doNothing;

    //    public override void OnEnter()
    //    {
    //        base.OnEnter();
    //        anim = GetModelAnimator();

    //        shiggyCon = characterBody.GetComponent<ShiggyController>();
    //        doNothing = false;
    //    }

    //    public override void FixedUpdate()
    //    {
    //        base.FixedUpdate();
    //        if (isAuthority)
    //        {
    //            if (shiggyCon) 
    //            {
    //                if (!shiggyCon.flightExpired || characterMotor.isGrounded || characterMotor.velocity.y > 0)
    //                {
    //                    doNothing = true;
    //                }
    //                else 
    //                {
    //                    doNothing = false;
    //                }
    //            }

    //            if (!doNothing) 
    //            {
    //                float newFallingVelocity = characterMotor.velocity.y / 1.5f;
    //                if (newFallingVelocity > slowestDescent)
    //                {
    //                    newFallingVelocity = slowestDescent;
    //                }
    //                newFallingVelocity = Mathf.MoveTowards(
    //                    newFallingVelocity,
    //                    Modules.Config.glideSpeed.Value,
    //                    Modules.Config.glideAcceleration.Value * Time.fixedDeltaTime);
    //                characterMotor.velocity = new Vector3(characterMotor.velocity.x, newFallingVelocity, characterMotor.velocity.z);
    //            }
    //        }
    //    }

    //    public override void OnExit()
    //    {
    //        base.OnExit();
    //    }

    //    public override InterruptPriority GetMinimumInterruptPriority()
    //    {
    //        return InterruptPriority.Frozen;
    //    }
    //}
}
