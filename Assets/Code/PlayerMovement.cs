using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5;
    Vector2 movementVector;
    public Vector2 jumpHeight;
    public bool canJump = true;
    float currentSpeed; //will use for modifiers

    Vector3 RestartPosition;

    public Rigidbody2D body;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        RestartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (movementVector.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movementVector.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (canJump && Input.GetButtonDown("Jump"))
        {
            //Add force to the player
            body.AddForce(jumpHeight, ForceMode2D.Impulse);
            canJump = false;
        }

        movementVector.y = body.velocity.y;
        body.velocity = movementVector;
    }

    private void FixedUpdate()
    {
        //movement
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector *= movementSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contactCount > 0) //points at which the player hits another object
        {
            ContactPoint2D contact = collision.contacts[0]; //get me the first contact
            
            //normal is the surface direction, Vector2.up
            if (Vector2.Dot(contact.normal, Vector2.up) > 0.25)
            {
                canJump = true;
            }
            else
            {
                canJump = false;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canJump = false;
    }

}
