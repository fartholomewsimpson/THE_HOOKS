using UnityEngine;
using StateStuff.States;

namespace StateStuff
{
    public class ControlHandler : MonoBehaviour
    {
        new Rigidbody2D rigidbody;
        ControlState currentState;

        Animator animator;
        SpriteRenderer spriteRenderer;

        public ControlState CurrentState { get { return currentState; } }

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            currentState = new StandingState();

            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            currentState = currentState.Update(rigidbody.velocity);
        }

        void FixedUpdate()
        {
            currentState.FixedUpdate(rigidbody);

            animator.SetFloat("XVelocity", Mathf.Abs(rigidbody.velocity.x));
            animator.SetFloat("YVelocity", rigidbody.velocity.y);

            if (rigidbody.velocity.x >= 0)
                spriteRenderer.flipX = false;
            if (rigidbody.velocity.x < 0)
                spriteRenderer.flipX = true;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            currentState.HandleCollision(collision);
        }
    }
}
