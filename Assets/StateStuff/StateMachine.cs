using UnityEngine;
using StateStuff.States;

namespace StateStuff
{
    public class StateMachine : MonoBehaviour
    {
        private Rigidbody2D rb;
        private State currentState;

        public State CurrentState { get { return currentState; } }

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            currentState = new StandingState();
        }

        void Update()
        {
            currentState = currentState.Update(rb);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var listener = currentState as ICollisionListener;
            if (listener != null)
                listener.HandleCollision(collision);
        }
    }
}
