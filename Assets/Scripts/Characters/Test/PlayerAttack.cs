using Hokuro.Functions;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerBase))]
public class PlayerAttack : GenericDamage
{
    private PlayerBase playerBase;
    private PlayerInput playerInput;
    private Rigidbody2D rb2d;
    private Animator anim;
    private AnimatorStateInfo stateInfo;

    private bool attacking = false;
    private int attackNum = 0;
    private float attackAnimTimer = 0.7f;
    private float remainAnimTimer = 0f;
    private readonly string attackTag = "Attack";
    private readonly string attack_land = "Attack_Land";
    private readonly string attack_land_end = "Attack_Land_End";

    private void Awake()
    {
        playerBase = GetComponentInParent<PlayerBase>();
        playerInput = GetComponentInParent<PlayerInput>();
        rb2d = GetComponentInParent<Rigidbody2D>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        CheckAnimTimer();

        if (playerInput.HandleAttack() && !playerBase.IsClimbing())
        {
            playerBase.SetState(State.attacking);
            playerBase.PlayAttackAnim();
            TimerFunction.Create(() =>
            {
                playerBase.SetState(State.moving);
                damaged = false;
            }, 0.433f, "Attack");
        }

        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        attacking = stateInfo.IsTag(attackTag);

        if (!damaged)
        {
            if (attacking)
            {
                if (stateInfo.IsName(attack_land))
                {
                    Attack();
                }
                else
                {
                    float playbackTime = stateInfo.normalizedTime;

                    if (0.33f < playbackTime && playbackTime < 0.66f)
                    {
                        Attack();
                        if (playerBase.IsGrounded()) CheckAttackAnim();
                    }
                }

                float velocity = (playerBase.IsGrounded() || stateInfo.IsName(attack_land_end)) ? 0f : rb2d.velocity.x;
                rb2d.velocity = new Vector2(velocity, rb2d.velocity.y);
            }
        }
    }
    
    private void CheckAnimTimer()
    {
        if ((remainAnimTimer += Time.deltaTime) > attackAnimTimer)
        {
            attackNum = 0;
            playerBase.SetAttackAnim(attackNum);
        }
        
        if (attacking)
        {
            remainAnimTimer = 0f;
        }
    }

    private void CheckAttackAnim()
    {
        damaged = damage != null ? true : false;

        if (damaged)
        {
            attackNum = (attackNum + 1) % 3;
        }
        else
        {
            attackNum = 0;
        }

        playerBase.SetAttackAnim(attackNum);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (attacking)
        {
            Gizmos.color = damaged ? Color.red : Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }
#endif
}