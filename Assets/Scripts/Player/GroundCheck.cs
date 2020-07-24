using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Animator anim;
    public LayerMask platform;
    private BoxCollider2D col;
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        col = GetComponentInParent<BoxCollider2D>();
    }
    /*
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("Grounded", true);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("Grounded", false);
        }
    }*/

    private void Update()
    {     
        RaycastHit2D hit = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, platform);
        anim.SetBool("Grounded", hit.collider != null);
    }

}
