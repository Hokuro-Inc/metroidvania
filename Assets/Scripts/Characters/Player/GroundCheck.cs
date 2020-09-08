using Hokuro.Functions;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Tooltip("Capa de las colisiones")]
    [SerializeField] private LayerMask groundLayer;

    private PlayerBase playerBase;
    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider;
    private CircleCollider2D circleCollider;
    private Transform floorCheck;
    private Transform wallCheck;
    private Transform ledgeCheck;
    private RaycastHit2D floorHit;
    private RaycastHit2D wallHit;
    private RaycastHit2D cornerHit;
    private int groundLayerIndex;
    private bool cornerDetected = false;
    private bool canClimb = false;
    private int direction;
    private Vector2 ledgePos;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;
    private float wallDistance = 0.3f;
    private float ledgeOffsetX1 = 0.5f;
    private float ledgeOffsetY1 = 0f;
    private float ledgeOffsetX2 = 0.5f;
    private float ledgeOffsetY2 = 2.12f;

    private void Awake()
    {
        playerBase = GetComponentInParent<PlayerBase>();
        rb2d = GetComponentInParent<Rigidbody2D>();
        boxCollider = GetComponentInParent<BoxCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        floorCheck = transform.GetChild(0).GetComponent<Transform>();
        wallCheck = transform.GetChild(1).GetComponent<Transform>();
        ledgeCheck = transform.GetChild(2).GetComponent<Transform>();
    }

    private void Start()
    {
        groundLayerIndex = LayerMask.NameToLayer("Ground");
        circleCollider.enabled = false;
    }

    private void Update()
    {
        direction = (int)playerBase.transform.localScale.x;
        if (!playerBase.IsClimbingLedge()) CheckFloor();
        CheckWall();
        if (!playerBase.IsGrounded()) CheckCorner();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == groundLayerIndex && circleCollider.IsTouching(other)) playerBase.PlayCrouchAnim(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == groundLayerIndex && !circleCollider.IsTouching(other))
        {
            playerBase.PlayCrouchAnim(false);
            playerBase.SetColliderNormalSize();
            circleCollider.enabled = false;
        }
    }

    private void CheckFloor()
    {
        floorHit = Physics2D.BoxCast(floorCheck.position, new Vector2(0.8f, 0.1f), 0f, Vector2.down, 0.1f, groundLayer);
        playerBase.PlayJumpAnim(floorHit.collider != null);
    }

    private void CheckWall()
    {
        wallHit = Physics2D.Raycast(wallCheck.position, Vector2.right * direction, wallDistance, groundLayer);
        playerBase.PlayWallSlideAnim(wallHit.collider != null);
    }

    private void CheckCorner()
    {
        cornerHit = Physics2D.Raycast(ledgeCheck.position, Vector2.right * direction, wallDistance, groundLayer);

        if (wallHit.collider != null && cornerHit.collider == null && !cornerDetected)
        {
            rb2d.velocity = Vector2.zero;
            cornerDetected = true;
            ledgePos = wallCheck.position;
        }

        if (cornerDetected && !canClimb)
        {
            TimerFunction.Create(() => FinishClimb(), 0.72f, "Climb");
            boxCollider.enabled = false;
            canClimb = true;

            if (direction > 0)
            {
                ledgePos1 = new Vector2(Mathf.Floor(ledgePos.x + wallDistance) - ledgeOffsetX1, Mathf.Floor(ledgePos.y) + ledgeOffsetY1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePos.x + wallDistance) + ledgeOffsetX2, Mathf.Floor(ledgePos.y) + ledgeOffsetY2);
            }
            else
            {
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePos.x - wallDistance) + ledgeOffsetX1, Mathf.Floor(ledgePos.y) + ledgeOffsetY1);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePos.x - wallDistance) - ledgeOffsetX2, Mathf.Floor(ledgePos.y) + ledgeOffsetY2);
            }

            playerBase.PlayLadderClimbAnim(false);
            playerBase.PlayLedgeClimbAnim(true);
        }

        if (canClimb)
        {
            playerBase.transform.position = ledgePos1;
        }
    }

    private void FinishClimb()
    {
        playerBase.PlayLedgeClimbAnim(false);
        playerBase.transform.position = ledgePos2;
        boxCollider.enabled = true;
        canClimb = false;
        cornerDetected = false;
        playerBase.SetColliderNormalSize();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (floorCheck == null) return;

        Color groundColor = floorHit.collider != null ? Color.red : Color.green;
        Gizmos.color = groundColor;
        Gizmos.DrawWireCube(floorCheck.position, new Vector2(0.8f, 0.1f));

        Color wallColor = wallHit.collider != null ? Color.red : Color.green;
        Debug.DrawRay(wallCheck.position, Vector2.right * direction * wallDistance, wallColor);

        Color cornerColor = cornerHit.collider != null ? Color.red : Color.green;
        Debug.DrawRay(ledgeCheck.position, Vector2.right * direction * wallDistance, cornerColor);
    }
#endif
}