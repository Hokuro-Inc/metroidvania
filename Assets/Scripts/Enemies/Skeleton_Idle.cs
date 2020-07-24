using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Idle : Enemy
{
    Vector3 target;

    Animator anim;
    //Rigidbody2D rb;

    private bool chasing = false;
    private bool can_attack = true;
    private bool is_attacking;

    void Start()
    {
        anim = GetComponent<Animator>();
        target = homePosition;
    }

    void Update()
    {
        target = LookForTarget();
        LookAtTarget(target);
        if(currentState != State.attacking)
        {
            if (Vector3.Distance(transform.position, target) > 0.15f)
            {
                anim.SetBool("moving", true);
                transform.Translate(GetDirection() * speed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("moving", false);
                transform.Translate(Vector2.zero);
            }
        }
        
        if (Vector3.Distance(pivot.transform.position, player.transform.GetChild(0).position) <= attack_radius && can_attack)
        {
            Attack();
            anim.SetTrigger("attacking");
            can_attack = false;
        }
        else
        {
            currentState = State.idle;
        }
        if((attack_rate_timer -= Time.deltaTime) < 0f)
        {
            can_attack = true;
            attack_rate_timer = attack_speed;
        }
    }
}
