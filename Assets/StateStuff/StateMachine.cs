using UnityEngine;
using StateStuff.States;

namespace StateStuff
{
    public class StateMachine : MonoBehaviour
    {
        new Rigidbody2D rigidbody;
        State currentState;

        // TODO: Move these if possible?
        Animator animator;
        SpriteRenderer spriteRenderer;

        public State CurrentState { get { return currentState; } }

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            currentState = new StandingState();

            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void FixedUpdate()
        {
            currentState = currentState.Update(rigidbody);
            animator.SetFloat("Velocity", Mathf.Abs(rigidbody.velocity.x));
            if (rigidbody.velocity.x > 0)
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
