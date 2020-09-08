using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerBase))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float climbSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashForce;

    private PlayerBase playerBase;
    private PlayerInput playerInput;
    private Rigidbody2D rb2d;
    private CircleCollider2D circleCollider;
    private float direction;
    private float speedMultiplier = 1f;
    private bool dashing = false;
    private float dashRemainForce;

    private void Awake()
    {
        playerBase = GetComponent<PlayerBase>();
        playerInput = GetComponent<PlayerInput>();
        rb2d = GetComponent<Rigidbody2D>();
        circleCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        circleCollider.enabled = false;
        dashRemainForce = dashForce;
    }

    private void Update()
    {
        /*if (playerBase.GetState() == State.moving && !playerBase.IsClimbing())
        {
            Move();
            Jump();
            if (!playerBase.IsTouchingWall()) Dash();            
        }
        else if (playerBase.IsClimbingLadder())
        {
            ClimbLadder();
        }*/
        if (playerBase.GetState() == State.moving)
        {
            if (!playerBase.IsClimbing()) Move();
            if (!playerBase.IsClimbingLedge()) Jump();
            if (!playerBase.IsTouchingWall()) Dash();
            if (playerBase.IsClimbingLadder()) ClimbLadder();
        }
    }

    private void FixedUpdate()
    {
        if (dashing)
        {
            if ((dashRemainForce -= Time.fixedDeltaTime * 150f) > 0f)
            {
                rb2d.velocity = dashRemainForce * transform.localScale.x * Vector2.right;
                circleCollider.enabled = dashRemainForce < 2f ? true : false;
            }
            else
            {                
                dashing = false;
                dashRemainForce = dashForce;
                playerBase.PlayDashAnim(false);
                playerBase.SetColliderNormalSize();
                if (!circleCollider.IsTouchingLayers()) circleCollider.enabled = false;
            }
        }
    }

    private void Move()
    {
        if (!dashing)
        {
            direction = playerInput.HandleMovement();
            speedMultiplier = 1f;

            if (playerBase.IsCrouching()) speedMultiplier = 0.75f;

            if (playerBase.IsGrounded())
            {
                rb2d.velocity = new Vector2(direction * movementSpeed * speedMultiplier, rb2d.velocity.y);
            }

            playerBase.PlayMoveAnim(rb2d.velocity.x);
        }
    }

    private void Jump()
    {
        Debug.Log("entra");
        if (playerInput.HandleJump())
        {
            Debug.Log("pressed");
            if (playerBase.IsClimbingLadder())
            {
                Debug.Log("climbing");
                playerBase.PlayLadderClimbAnim(false);
            }

            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }

        if (!playerBase.IsGrounded())
        {
            speedMultiplier = 3f;
            rb2d.velocity += new Vector2(direction * movementSpeed * speedMultiplier * Time.deltaTime, 0f);
            rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -movementSpeed, movementSpeed), rb2d.velocity.y);
        }
    }

    private void Dash()
    {
        if (playerInput.HandleDash())
        {
            dashing = true;
            playerBase.PlayDashAnim(true);
            playerBase.SetColliderDashingSize();
        }
    }

    private void ClimbLadder()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb2d.velocity = Vector2.up * climbSpeed;
        }        
        else if (Input.GetKey(KeyCode.S))
        {
            rb2d.velocity = Vector2.down * climbSpeed;
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }
}