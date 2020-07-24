using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum State
    {
        idle,
        walking,
        attacking,
        stunned,
        patrolling,
        chasing
    }

    [Header("Sight")]
    public Transform pivot;

    [Header("Attributes")]
    public FloatValue maxHealth;
    public float speed;
    [Range(0, 360)]
    public float maxAngle;
    public float maxRadius;
    public float attack_radius;
    public float attack_damage;
    public float attack_speed;
    public float direction_timer;
    public float investigating_timer;

    [Header("State")]
    public State currentState;

    [Header("Player")]
    public GameObject player;

    private bool facing_right = true;
    protected float change_direction_timer;
    protected float remaining_investigating_timer;
    protected float attack_rate_timer;

    protected Vector3 homePosition;

    private float health;

    void Awake()
    {
        change_direction_timer = direction_timer;
        attack_rate_timer = attack_speed;

        homePosition = transform.position;

        health = maxHealth.initialValue;
    }

    public void UpdateCharacterDirection()
    {
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

    public Vector3 LookForTarget()
    {
        Vector3 tmp;

        if (CanSeePlayer())
        {
            tmp = player.transform.position;
            remaining_investigating_timer = investigating_timer;
            currentState = State.chasing;
        }
        else if (!CanSeePlayer() && (remaining_investigating_timer -= Time.deltaTime) > 0f)
        {
            tmp = player.transform.position;
        }
        else
        {
            tmp = homePosition;
            if(Vector2.Distance(transform.position, homePosition) > 0.1f)
            {
                currentState = State.walking;
            }
            else
            {
                currentState = State.idle;
            }
        }
        return tmp;
    }

    public bool CanSeePlayer()
    {
        //Debug.Log("entra");
        Vector3 direction = player.transform.GetChild(0).position - pivot.transform.position;

        if (Vector3.Angle(direction, pivot.forward) <= maxAngle && Vector3.Distance(pivot.position, player.transform.GetChild(0).position) <= maxRadius)
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

    public void Attack()
    {
        currentState = State.attacking;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            this.gameObject.SetActive(false);
        }
    }
}