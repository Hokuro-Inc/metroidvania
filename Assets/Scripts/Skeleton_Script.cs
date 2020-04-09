using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Script : Enemies
{
    [Header("Patrolling variables")]
    public Transform patrolling_spot;

    private Vector3 start;
    private Vector3 end;
    private Vector3 target;

    Animator anim;

    private float investigating_time;

    bool chasing = false;

    void Start()
    {
        if (patrolling_spot != null)
        {
            patrolling_spot.parent = null;
            start = transform.position;
            end = target = patrolling_spot.transform.position;
        }
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            chasing = true;
            StopCoroutine(WaitTimer(target));
            target = player.transform.position;
            investigating_time = 3f;
        }
        else if(!CanSeePlayer() && investigating_time > 0f)
        {
            Debug.Log(investigating_time);
            target = player.transform.position;
            investigating_time -= Time.deltaTime;
        }
        else
        {
            chasing = false;
            target = patrolling_spot.transform.position;
            LookAtTarget(target);
        }

        if (chasing)
        {
            if(Vector3.Distance(transform.position, target) >= attack_radius)
            {
                anim.SetBool("moving", true);
            }
            else
            {
                anim.SetBool("moving", false);
            }
            LookAtTarget(target);
        }

        if (anim.GetBool("moving"))
        {
            transform.Translate(GetDirection() * speed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, target) < 0.1f && !chasing)
        {
            Debug.Log("entra");
            patrolling_spot.transform.position = (target == start) ? end : start;
            target = patrolling_spot.transform.position;
            StartCoroutine(WaitTimer(target));
        }
    }

    IEnumerator WaitTimer(Vector3 target)
    {
        anim.SetBool("moving", false);
        yield return new WaitForSeconds(2f);
        anim.SetBool("moving", true);
        LookAtTarget(target);
    }
}
