using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 10f;

    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
      
        
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);   //rb for GetComponent<Rigidbody2D>();
            //rb.AddForce(Vector2.up * jumpForce);
        }

        UpdateAnimationUpdate();

    }

    private void UpdateAnimationUpdate()
    {
        MovementState State;

        if (dirX > 0f)
        {
            State = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            State = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            State = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            State= MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            State= MovementState.falling;
        }

        anim.SetInteger("State", (int)State);
    }

  
}
