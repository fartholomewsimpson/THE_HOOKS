using UnityEngine;

namespace StateStuff.States
{
    public class StandingState : State
    {
        public StandingState() : base(StateName.STANDING) {}

        public override State Update(Rigidbody2D rigidbody)
        {
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);

            if (Input.GetKey(KeyCode.A))
            {
                return new WalkingState();
            }
            if (Input.GetKey(KeyCode.D))
            {
                return new WalkingState();
            }
            if (Input.GetKey(KeyCode.Space))
            {
                return new JumpingState();
            }
            return this;
        }
    }
}
