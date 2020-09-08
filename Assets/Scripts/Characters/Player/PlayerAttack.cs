using System.Collections;
using UnityEngine;

public class PlayerAttackTest : GenericDamage
{
    private readonly int attackingHash = Animator.StringToHash("Attacking");
    private readonly int crouchingHash = PlayerMovementTest.crouchingHash;

    private Rigidbody2D rb2d;
    private Animator anim;
    private CircleCollider2D col;
    private AnimatorStateInfo stateInfo;
    private PlayerMovementTest playerState;

    void Start()
    {
        rb2d = gameObject.GetComponentInParent<Rigidbody2D>();
        anim = gameObject.GetComponentInParent<Animator>();
        playerState = gameObject.GetComponentInParent<PlayerMovementTest>();
        col = gameObject.GetComponent<CircleCollider2D>();
        col.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(InputManager.keys["Attack"]) && !anim.GetBool(crouchingHash) && playerState.currentState != PlayerState.attacking)
        {
            rb2d.velocity = Vector3.zero;
            playerState.currentState = PlayerState.attacking;
            anim.SetTrigger(attackingHash);
            StartCoroutine(AttackCo());
        }

        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool attacking = stateInfo.IsName("Attack_0");

        if (attacking)
        {
            float playbackTime = stateInfo.normalizedTime;
            if (playbackTime > 0.33 && playbackTime < 0.66)
            {
                col.enabled = true;
            }
            else
            {
                col.enabled = false;
            }
        }
    }

    private IEnumerator AttackCo()
    {
        yield return new WaitForSeconds(0.4f);
        playerState.currentState = PlayerState.moving;
    }
}