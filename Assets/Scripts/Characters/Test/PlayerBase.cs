using UnityEngine;

public enum State
{
    moving,
    attacking,
    interacting
}

public class PlayerBase : MonoBehaviour
{
    private readonly int speedHash = Animator.StringToHash("Speed");
    private readonly int airSpeedHash = Animator.StringToHash("Air Speed");
    private readonly int attackNumHash = Animator.StringToHash("Attack Number");
    private readonly int groundedHash = Animator.StringToHash("Grounded");
    private readonly int crouchingHash = Animator.StringToHash("Crouching");
    private readonly int dashingHash = Animator.StringToHash("Dashing");
    private readonly int wallSlideHash = Animator.StringToHash("Wall Sliding");
    private readonly int climbLadderHash = Animator.StringToHash("Climbing Ladder");
    private readonly int climbLedgeHash = Animator.StringToHash("Climbing Ledge");
    private readonly int attackingHash = Animator.StringToHash("Attacking");

    private Animator anim;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2d;
    private State currentState;
    private float normalSizeY;
    private float dashingSizeY;
    private float normalOffsetY;
    private float dashingOffsetY;
    private bool isTouchingWall;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        currentState = State.moving;
        normalSizeY = boxCollider.size.y;
        normalOffsetY = boxCollider.offset.y;
        dashingSizeY = 1f;
        dashingOffsetY = -0.58f;
    }

    public void SetState(State newState)
    {
        currentState = newState;
    }

    public State GetState()
    {
        return currentState;
    }

    public bool IsGrounded()
    {
        return anim.GetBool(groundedHash);
    }

    public bool IsDashing()
    {
        return anim.GetBool(dashingHash);
    }

    public bool IsCrouching()
    { 
        return anim.GetBool(crouchingHash);
    }

    public bool IsClimbing()
    {
        return anim.GetBool(climbLadderHash) || anim.GetBool(climbLedgeHash);
    }

    public bool IsClimbingLadder()
    {
        return anim.GetBool(climbLadderHash);
    }

    public bool IsClimbingLedge()
    {
        return anim.GetBool(climbLedgeHash);
    }

    public bool IsTouchingWall()
    {
        return isTouchingWall;
    }

    public void PlayMoveAnim(float direction)
    {
        anim.SetFloat(speedHash, isTouchingWall ? 0f : Mathf.Abs(direction));

        if (direction > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void PlayJumpAnim(bool grounded)
    {
        anim.SetBool(groundedHash, grounded);
        anim.SetFloat(airSpeedHash, rb2d.velocity.y);
    }

    public void PlayWallSlideAnim(bool touchingWall)
    {
        isTouchingWall = touchingWall;

        if (isTouchingWall && !anim.GetBool(groundedHash) && anim.GetFloat(airSpeedHash) < 0f)
        {
            anim.SetBool(wallSlideHash, true);
        }
        else
        {
            anim.SetBool(wallSlideHash, false);
        }
    }

    public void PlayLadderClimbAnim(bool climbingLadder)
    {
        anim.SetBool(climbLadderHash, climbingLadder);
        rb2d.isKinematic = climbingLadder;
    }

    public void PlayLedgeClimbAnim(bool climbingLedge)
    {
        anim.SetBool(climbLedgeHash, climbingLedge);
    }

    public void PlayDashAnim(bool dashing)
    {
        anim.SetBool(dashingHash, dashing);
    }

    public void PlayCrouchAnim(bool crouching)
    {
        anim.SetBool(crouchingHash, crouching);
    }

    public void SetAttackAnim(int attackNum)
    {
        anim.SetInteger(attackNumHash, attackNum);
    }

    public void PlayAttackAnim()
    {
        anim.SetTrigger(attackingHash);
    }

    public void SetColliderNormalSize()
    {
        if (anim.GetBool(dashingHash) || anim.GetBool(crouchingHash)) return;
        boxCollider.size = new Vector2(boxCollider.size.x, normalSizeY);
        boxCollider.offset = new Vector2(boxCollider.offset.x, normalOffsetY);
    }

    public void SetColliderDashingSize()
    {
        boxCollider.size = new Vector2(boxCollider.size.x, dashingSizeY);
        boxCollider.offset = new Vector2(boxCollider.offset.x, dashingOffsetY);
    }
}