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

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed;
    public float speed;
    public float jumpPower;
    public float dashPower;
    public PlayerState currentState;
    /*public ExtendedFloatValue currentMana;
    public Signal ManaSignal;
    public Inventory playerInventory;*/

    //public bool grounded;
    private bool jump = false;
    private float normalSizeY;
    private float dashingSizeY;
    private float normalOffsetY;
    private float dashingOffsetY;

    private readonly int speedHash = Animator.StringToHash("Speed");
    public static readonly int groundedHash = Animator.StringToHash("Grounded");
    public static readonly int crouchingHash = Animator.StringToHash("Crouching");
    private readonly int dashingHash = Animator.StringToHash("Dashing");

    private Rigidbody2D rb2d;
    private Animator anim;
    //private AnimatorStateInfo stateInfo;
    private BoxCollider2D col;
    private BoxCollider2D hurtbox;

    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        col = gameObject.GetComponent<BoxCollider2D>();
        hurtbox = gameObject.transform.GetChild(3).GetComponent<BoxCollider2D>();

        currentState = PlayerState.moving;
        normalSizeY = col.size.y;
        normalOffsetY = col.offset.y;
        dashingSizeY = 1f;
        dashingOffsetY = -0.55f;
    }


    void Update()
    {
        // Juego en pausa
        if (Time.timeScale == 0f)
        {
            return;
        }

        anim.SetFloat(speedHash, Mathf.Abs(rb2d.velocity.x));

        if (Input.GetKeyDown(InputManager.keys["Jump"]) && anim.GetBool(groundedHash) && !anim.GetBool(crouchingHash) && currentState == PlayerState.moving)
        {
            jump = true;
        }

        if (Input.GetKeyDown(InputManager.keys["Dash"]) && anim.GetBool(groundedHash) && !anim.GetBool(dashingHash) && currentState == PlayerState.moving)
        {
            anim.SetBool(dashingHash, true);
            col.size = new Vector2(col.size.x, dashingSizeY);
            col.offset = new Vector2(col.offset.x, dashingOffsetY);
            hurtbox.enabled = false;
        }
    }

    void FixedUpdate()
    {
        if (currentState == PlayerState.attacking)
        {
            return;
        }

        Vector3 easeVelocity = rb2d.velocity;
        
        easeVelocity.x *= 0.75f;

        float h = 0f;
        if (!anim.GetBool("Dashing"))
        {
            if (Input.GetKey(InputManager.keys["Left"]) || Input.GetKey(InputManager.keys["Right"]))
            {
                h = Input.GetKey(InputManager.keys["Right"]) ? 1f : -1f;
            }
        }

        float peso = rb2d.mass;

        if (anim.GetBool(groundedHash))
        {
            rb2d.velocity = easeVelocity;
        }

        if (anim.GetBool(crouchingHash))
        {
            rb2d.velocity *= 0.75f;
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

        if (anim.GetBool(dashingHash))
        {
            rb2d.AddForce(Vector2.right * dashPower * transform.localScale.x, ForceMode2D.Impulse);
            StartCoroutine(DashCo());
        }

        if (!anim.GetBool(crouchingHash) && !anim.GetBool(dashingHash))
        {
            col.size = new Vector2(col.size.x, normalSizeY);
            col.offset = new Vector2(col.offset.x, normalOffsetY);
        }

        if (jump)
        {
            jump = false;
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    IEnumerator DashCo()
    {
        yield return new WaitForSeconds(0.3f);
        anim.SetBool(dashingHash, false);
        hurtbox.enabled = true;
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
    }

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
    }*/
}

