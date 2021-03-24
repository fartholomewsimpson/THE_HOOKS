using UnityEngine;

namespace StateStuff.States
{
    public class StandingState : State
    {
        public StandingState() : base(StateName.STANDING) {}

        public override State Update(Rigidbody2D rigidbody)
        {
            if (Input.GetKey(KeyCode.A))
            {
                return new WalkingState();
            }
            if (Input.GetKey(KeyCode.D))
            {
                return new WalkingState();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                return new JumpingState();
            }
            return this;
        }
    }
}
