using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform patrol;
    public Transform pivot;

    public float speed;
    public float maxAngle;
    public float maxRadius;
    public float attack_radius;
    public float pause;
    public float investigation_time;

    private Vector3 start;
    private Vector3 end;
    private Vector3 target;

    private Animator anim;
    private Collider2D col;

    private float investigation_time_remaining = 0;

    GameObject player;

    void Start()
    {
        if(patrol != null)
        {
            patrol.parent = null;
            start = transform.position;
            end = patrol.position;
            target = patrol.position;
        }
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        //Debug.Log(target);
        if (CanSeePlayer())
        {
            StopCoroutine(WaitTimer());
            anim.SetBool("moving", true); // resetea el movimiento por si estaba parado
            target = player.transform.position;
            //target.x -= col.offset.x/2;
            investigation_time_remaining = investigation_time;
            if(Vector3.Distance(target, pivot.transform.position) < attack_radius)
            {
                anim.Play("Skeleton_Walk", -1, 0);
            }
            else
            {
                anim.SetBool("moving", true);
            }
        }
        else if(!CanSeePlayer() && investigation_time_remaining > 0f)
        {
            target = player.transform.position;
            //target.x -= col.offset.x/2;
            investigation_time_remaining -= Time.deltaTime;
        }
        else
        {
            target = patrol.position;
        }
    }

    void FixedUpdate()
    {
        if (!anim.GetBool("moving"))
        {
            return;
        }

        if(patrol != null)
        {
            float fixedSpeed = speed * Time.fixedDeltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);
        }

        if (target.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (target.x > transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        if (transform.position == patrol.position)
        {
            patrol.position = (target == start) ? end : start;
            StartCoroutine(WaitTimer());
        }
    }

    IEnumerator WaitTimer()
    {
        anim.SetBool("moving", false);
        yield return new WaitForSeconds(pause);
        anim.SetBool("moving", true);

        /*if (target.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (target.x > transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }*/
    }

    /*public static bool InFov(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
    {
        Collider2D[] overlaps = new Collider2D[10];
        //int count = Physics2D.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);
        int count = Physics2D.OverlapCircleNonAlloc(checkingObject.position, maxRadius, overlaps);

        for(int i = 0; i < count + 1; i++)
        {
            if(overlaps[i] != null)
            {
                if (overlaps[i].transform == target)
                {
                    Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                    directionBetween.z *= 0;

                    float angle = Vector3.Angle(checkingObject.forward, directionBetween);
                    if(angle <= maxAngle)
                    {
                        Debug.Log("hola3");
                        Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
                        RaycastHit hit;

                        if(Physics.Raycast(ray, out hit, maxRadius))
                        {
                            Debug.Log("hola4");
                            if (hit.transform == target)
                            {
                                Debug.Log("Player in sight");
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }*/

    bool CanSeePlayer()
    {
        //Debug.Log("entra");
        Vector3 direction = player.transform.position - pivot.transform.position;

        if (Vector3.Angle(direction, pivot.forward) <= maxAngle && Vector3.Distance(pivot.position, player.transform.position) <= maxRadius)
        {
            //Debug.Log("en campo");
            RaycastHit2D hit = Physics2D.Raycast(pivot.transform.position, direction, maxRadius, 1 << LayerMask.NameToLayer("Default"));
            if (hit.collider.CompareTag("Player"))
            {
                //Debug.Log("visto");
                return true;
            }
        }
        return false;
    }
}
