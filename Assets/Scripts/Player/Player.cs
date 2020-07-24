using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    moving,
    attacking,
    interacting,
    staggered
}

public class Player : MonoBehaviour
{
    public float maxSpeed;
    public float speed;
    public float jumpPower;
    public PlayerState currentState;
    public ExtendedFloatValue currentMana;
    public Signal ManaSignal;
    public Inventory playerInventory;

    //public bool grounded;
    bool jump;

    private Rigidbody2D rb2d;
    private Animator anim;
    private AnimatorStateInfo stateInfo;
    private CircleCollider2D col;

    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        currentState = PlayerState.moving;
        col = transform.GetChild(1).GetComponent<CircleCollider2D>();
        col.enabled = false;
    }


    void Update()
    {
        // Juego en pausa
        if (Time.timeScale == 0f)
        {
            return;
        }

        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
                
        if (Input.GetKeyDown(InputManager.keys["Jump"]) && anim.GetBool("Grounded") && currentState == PlayerState.moving)
        {
            jump = true;
        }

        if (Input.GetKeyDown(InputManager.keys["Attack"]) && currentState != PlayerState.attacking)
        {
            rb2d.velocity = Vector3.zero;
            currentState = PlayerState.attacking;
            anim.SetTrigger("Attacking");
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
        currentState = PlayerState.moving;
    }

    void FixedUpdate()
    {
        if (currentState == PlayerState.attacking)
        {
            return;
        }

        Vector3 easeVelocity = rb2d.velocity;
        
        easeVelocity.x *= 0.75f;

        float h;
        if (Input.GetKey(InputManager.keys["Left"]) || Input.GetKey(InputManager.keys["Right"]))
        {
            h = Input.GetKey(InputManager.keys["Right"]) ? 1f : -1f;
        }
        else
        {
            h = 0f;
        }

        float peso = rb2d.mass;

        if (anim.GetBool("Grounded"))
        {
            rb2d.velocity = easeVelocity;
        }

        rb2d.AddForce(((Vector2.right * speed) * h) * peso);
        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        if (h > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (h < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (jump)
        {
            jump = false;
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    /*public void UpdateHealth(float healthAmount)
    {
        // Positivo para curar y negativo para dañar
        currentHP.runtimeValue += healthAmount;
        currentHP.runtimeValue = Mathf.Clamp(currentHP.runtimeValue, 0, currentHP.maxValue);
        HealthSignal.Raise();
        if (currentHP.runtimeValue == 0f)
        {
            this.gameObject.SetActive(false);
        }
    }*/

    public void UpdateMana(float manaAmount)
    {
        // Positivo para curar y negativo para dañar
        currentMana.runtimeValue += manaAmount;
        currentMana.runtimeValue = Mathf.Clamp(currentMana.runtimeValue, 0, currentMana.maxValue);
        ManaSignal.Raise();
    }

    public void ReceiveItem()
    {
        if(playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interacting)
            {
                currentState = PlayerState.interacting;
            }
            else
            {
                currentState = PlayerState.moving;
                playerInventory.currentItem = null;
            }
        }
    }
}

