﻿using UnityEngine;

namespace StateStuff.States
{
    public class StandingState : ControlState
    {
        public StandingState() : base(StateName.STANDING) {}

        public override ControlState Update(Rigidbody2D rigidbody)
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
