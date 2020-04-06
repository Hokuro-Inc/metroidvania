using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Transform target;
    public Transform from;
    public Transform to;

    public float speed;

    private Vector3 start, end;

    void Start()
    {
        if(target != null)
        {
            target.parent = null;
            start = transform.position;
            end = target.position;
        }
    }

    void OnDrawGizmosSelected()
    {
        if(from != null && to != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(from.position, to.position);
            Gizmos.DrawSphere(from.position, 0.15f);
            Gizmos.DrawSphere(to.position, 0.15f);
        }
    }

    void FixedUpdate()
    {
        if(target != null)
        {
            float fixedSpeed = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, fixedSpeed);
        }

        if(transform.position == target.position)
        {
            target.position = (target.position == start) ? end : start;
        }

        if(target.position == start)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if(target.position == end)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    
}
