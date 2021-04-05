using UnityEngine;

namespace StateStuff.Utils
{
    public class MoveHandler
    {
        public float Increment { get; private set; } = 4;

        float inputDirection;
        float speed;

        public MoveHandler() {}

        public MoveHandler(float increment)
        {
            Increment = increment;
        }

        public void Update(Vector2 velocity)
        {
            if (Input.GetKey(KeyCode.A))
                inputDirection = -1;
            if (Input.GetKey(KeyCode.D))
                inputDirection = 1;
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                inputDirection = 0;
        }

        public void FixedUpdate(Rigidbody2D rigidbody)
        {
            rigidbody.AddForce(new Vector2(inputDirection * Increment * rigidbody.drag, 0));
        }
    }
}
