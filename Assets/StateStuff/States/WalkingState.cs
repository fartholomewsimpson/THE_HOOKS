using StateStuff.Utils;
using UnityEngine;

namespace StateStuff.States
{
    public class WalkingState : ControlState
    {
        float increment = .2f;
        float maxSpeed = 25;
        MoveHandler moveHandler;

        public WalkingState() : base(StateName.WALKING)
        {
            moveHandler = new MoveHandler(increment, maxSpeed);
        }

        public override ControlState Update(Rigidbody2D rigidbody)
        {
            moveHandler.Update(rigidbody);
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
    }
}
