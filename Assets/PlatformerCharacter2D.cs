using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float maxSpeed = 10f;                    
        [SerializeField] private float jumpForce = 400f;                 
        [SerializeField] private bool airControl = false;                 
        [SerializeField] private LayerMask whatIsGround;                  

        private Transform groundCheck;    
        const float groundedRadius = .2f;
        private bool grounded;            
        private Transform ceilingCheck;  
        const float ceilingRadius = .01f; 
        private Animator animator; 
        private Rigidbody2D rigidbody2D;
        private bool facingRight = true;
        private SpriteRenderer sprite;

        private void Awake()
        {
            groundCheck = transform.Find("GroundCheck");
            ceilingCheck = transform.Find("CeilingCheck");
            animator = GetComponent<Animator>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
        }


        private void FixedUpdate()
        {
            grounded = false;

            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    grounded = true;
            }
            animator.SetBool("Ground", grounded);

        }


        public void Move(float move, bool jump)
        {
            if (grounded || airControl)
            {
                animator.SetFloat("Speed", Mathf.Abs(move));

                rigidbody2D.velocity = new Vector2(move*maxSpeed, rigidbody2D.velocity.y);

                if (move > 0 && !facingRight)
                {
                    Flip();
                }
                else if (move < 0 && facingRight)
                {
                    Flip();
                }
            }
            
            if (grounded && jump && animator.GetBool("Ground"))
            {
                grounded = false;
                animator.SetBool("Ground", false);
                rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            }
        }


        private void Flip()
        {
            facingRight = !facingRight;
            sprite.flipX = !sprite.flipX;
        }

        public bool isGrounded()
        {
            if (grounded)
                return true;
            else
                return false;
        }
    }
}
