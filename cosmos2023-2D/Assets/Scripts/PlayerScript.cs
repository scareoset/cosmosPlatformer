using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // variables go here

    // friendship ended with charactercontroller
    // rigidbody2d is my new best friend
    public Rigidbody2D rigidbody;
    // for finding the Ground
    public Collider2D ground;
    // how fast our player will move!
    public float speed = 2.0f;
    // how high our player will jump!
    public float jump = 30.0f;
    // if the player is grounded or not!
    public bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 forceToAdd = new Vector2(Input.GetAxis("Horizontal") * speed, 0.0f);

        rigidbody.AddForce(forceToAdd, ForceMode2D.Force);

        if(Input.GetButton("Jump") && isGrounded)
        {
            rigidbody.AddForce(new Vector2(0.0f, jump), ForceMode2D.Force);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isGrounded = false;
        }
    }
}