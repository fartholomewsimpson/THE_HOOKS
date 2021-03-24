﻿using StateStuff.Utils;
using UnityEngine;

namespace StateStuff.States
{
    public class WalkingState : State
    {
        float increment = .1f;
        float maxSpeed = 5;
        MoveHandler moveHandler;

        public WalkingState() : base(StateName.WALKING)
        {
            moveHandler = new MoveHandler(increment, maxSpeed);
        }

        public override State Update(Rigidbody2D rigidbody)
        {
            moveHandler.Update(rigidbody);
            if (Input.GetKey(KeyCode.Space))
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
