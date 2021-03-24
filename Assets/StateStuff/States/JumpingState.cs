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
        bool grounded = false;
        MoveHandler moveHandler;

        public JumpingState() : base(StateName.JUMPING)
        {
            moveHandler = new MoveHandler(moveIncrement, maxMoveSpeed);
        }

        public override State Update(Rigidbody2D rigidbody)
        {
            moveHandler.Update(rigidbody);
            if (grounded)
            {
                if (rigidbody.velocity.x != 0)
                    return new WalkingState();
                return new StandingState();
            }
            else if (Input.GetKey(KeyCode.Space) && jumpAmount < maxJumpAmount)
            {
                grounded = false;
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
                jumpAmount++;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumpAmount = maxJumpAmount;
            }

            return this;
        }

        public override void HandleCollision(Collision2D collision)
        {
            if (collision.rigidbody.tag == "collidable")
            {
                grounded = true;
                jumpAmount = 0;
            }
        }
    }
}
