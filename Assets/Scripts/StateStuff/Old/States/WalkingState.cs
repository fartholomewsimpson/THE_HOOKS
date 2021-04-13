using StateStuff.Utils;
using UnityEngine;

namespace StateStuff.States
{
    public class WalkingState : ControlState
    {
        MoveHandler moveHandler;

        public WalkingState() : base(StateName.WALKING)
        {
            moveHandler = new MoveHandler();
        }

        public override ControlState Update(Vector2 velocity)
        {
            moveHandler.Update(velocity);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                return new JumpingState();
            }
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                return new StandingState();
            }

            return this;
        }

        public override void FixedUpdate(Rigidbody2D rigidbody)
        {
            moveHandler.FixedUpdate(rigidbody);
        }
    }
}
