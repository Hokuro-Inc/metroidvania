using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [Header("Sight")]
    public Transform pivot;

    [Header("Attributes")]
    public float maxHealth;
    public float speed;
    [Range(0, 360)]
    public float maxAngle;
    public float maxRadius;
    public float attack_radius;
    public float attack_damage;
    //public float pause;
    //public float investigation_time;

    //private Animator anim;

    //private float investigation_time_remaining = 0;

    //private bool chasing = false;

    [Header("Player")]
    public GameObject player;

    private bool facing_right = true;

    public void UpdateCharacterDirection()
    {
        /*if (target.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (target.x > transform.position.x)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }*/
        facing_right = !facing_right;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1f, 1f);
    }

    public Vector2 GetDirection()
    {
        return facing_right ? Vector2.right : Vector2.left;
    }

    public void LookAtTarget(Vector3 target)
    {
        if (target != null)
        {
            float DirX = target.x - transform.position.x;
            if (DirX < 0 && facing_right || DirX > 0 && !facing_right)
            {
                UpdateCharacterDirection();
            }
        }
    }

    public bool CanSeePlayer()
    {
        //Debug.Log("entra");
        Vector3 direction = player.transform.position - pivot.transform.position;

        if (Vector3.Angle(direction, pivot.forward) <= maxAngle && Vector3.Distance(pivot.position, player.transform.position) <= maxRadius)
        {
            //Debug.Log("en campo");
            RaycastHit2D hit = Physics2D.Raycast(pivot.transform.position, direction, maxRadius, 1 << LayerMask.NameToLayer("Default"));
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("visto");
                return true;
            }
        }
        return false;
    }
}
