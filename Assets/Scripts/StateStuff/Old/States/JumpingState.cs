using StateStuff.Utils;
using UnityEngine;

namespace StateStuff.States
{
    public class JumpingState : ControlState, ICollisionListener
    {
        float jumpSpeed = 15;
        int jumpAmount = 0;
        int maxJumpAmount = 30;
        bool grounded = false;
        bool jumpin = false;
        MoveHandler moveHandler;
        Animator animator;

        public JumpingState() : base(StateName.JUMPING)
        {
            moveHandler = new MoveHandler();
        }

        public override ControlState Update(Vector2 velocity)
        {
            moveHandler.Update(velocity);

            if (!Input.GetKey(KeyCode.Space))
                jumpAmount = maxJumpAmount;

            if (grounded)
            {
                if (velocity.x != 0)
                    return new WalkingState();
                return new StandingState();
            }
            else if (Input.GetKey(KeyCode.Space) && jumpAmount < maxJumpAmount)
            {
                grounded = false;
                jumpAmount++;
            }
            
            jumpin = jumpAmount < maxJumpAmount;

            return this;
        }

        public override void FixedUpdate(Rigidbody2D rigidbody)
        {
            moveHandler.FixedUpdate(rigidbody);

            if (jumpin)
            {
                rigidbody.velocity = new Vector2(
                    rigidbody.velocity.x,
                    jumpSpeed
                );
            }
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
