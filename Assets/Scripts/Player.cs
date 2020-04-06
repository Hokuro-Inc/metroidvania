using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed = 2f;
    public float speed = 50f;
    public float jumpPower = 25000f;

    public bool grounded;

    private Rigidbody2D rb2d;
    private Animator anim;

    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }


    void Update()
    {
        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));

        float peso = rb2d.mass;

        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if ((Input.GetButtonDown("Jump")) && (grounded == true))
        {
            rb2d.AddForce(Vector2.up * jumpPower * peso);
        }
    }

    void FixedUpdate()
    {
        Vector3 easeVelocity = rb2d.velocity;
        
        easeVelocity.x *= 0.75f;

        float h = Input.GetAxis("Horizontal");
        float peso = rb2d.mass;

        if (grounded)
        {
            rb2d.velocity = easeVelocity;
        }

        rb2d.AddForce(((Vector2.right * speed) * h) * peso);

        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.x < -maxSpeed)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }
    }
}

