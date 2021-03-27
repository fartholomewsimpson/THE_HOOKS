using UnityEngine;
using StateStuff.States;

namespace StateStuff
{
    public class StateMachine : MonoBehaviour
    {
        private new Rigidbody2D rigidbody;
        private State currentState;

        public State CurrentState { get { return currentState; } }

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            currentState = new StandingState();
        }

        void Update()
        {
            currentState = currentState.Update(rigidbody);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            currentState.HandleCollision(collision);
        }
    }
}
