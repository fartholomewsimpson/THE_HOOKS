using StateStuff.Utils;
using UnityEngine;

namespace StateStuff.States
{
    public class JumpingState : State, ICollisionListener
    {
        float moveIncrement = .1f;
        float maxMoveSpeed = 5;
        float jumpSpeed = 6;
        int jumpAmount = 0;
        int maxJumpAmount = 100;
        bool grounded = true;
        MoveHandler moveHandler;

        public JumpingState() : base(StateName.JUMPING)
        {
            moveHandler = new MoveHandler(moveIncrement, maxMoveSpeed);
        }

        public override State Update(Rigidbody2D rigidbody)
        {
            moveHandler.Update(rigidbody);
            if (Input.GetKey(KeyCode.Space) && jumpAmount < maxJumpAmount)
            {
                grounded = false;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
                jumpAmount++;
            }

            // TODO: How to determine if the user has let go of the space bar before transitioning back to
            //        this state to jump again?
            // I think it would have to be that
            // 1. States would need to presist, so the StateMachine would have to keep track of all states
            //     and switch between them instead of having them instantiate new ones.
            // 2. States would need to have a method that's called when entered to determine if they can
            //     actually enter the state or not (return a boolean maybe?) as part of the switching mechanism.
            // IS THERE ANY OTHER WAY?
            if (grounded)
            {
                if (rigidbody.velocity.x != 0)
                    return new WalkingState();
                return new StandingState();
            }
            return this;
        }

        public void HandleCollision(Collision2D collision)
        {
            if (collision.rigidbody.tag == "collidable")
            {
                grounded = true;
            }
        }
    }
}
