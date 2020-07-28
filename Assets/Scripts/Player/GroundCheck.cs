using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private readonly int groundedHash = PlayerMovement.groundedHash;
    private readonly int crouchingHash = PlayerMovement.crouchingHash;


    [Tooltip("Capa de las colisiones")]
    [SerializeField] private LayerMask layer;
    private Animator anim;
    private BoxCollider2D col;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        col = GetComponentInParent<BoxCollider2D>();
    }

    private void Update()
    {     
        RaycastHit2D hit = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, layer);
        anim.SetBool(groundedHash, hit.collider != null);

        hit = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.up, .4f, layer);
        anim.SetBool(crouchingHash, hit.collider != null);
    }

}
