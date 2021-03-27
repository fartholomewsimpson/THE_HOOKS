using UnityEngine;

namespace StateStuff.Utils
{
    public class MoveHandler
    {
        public float Increment { get; private set; }
        public float MaxSpeed { get; private set; }

        public MoveHandler(float increment, float maxSpeed)
        {
            Increment = increment;
            MaxSpeed = maxSpeed;
        }

        // TODO: Why does rigidbody.velocity.x slow on collision with the ground? How to fix?
        public void Update(Rigidbody2D rigidbody)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rigidbody.velocity = new Vector2(
                    Mathf.Max((rigidbody.velocity.x - Increment), -MaxSpeed),
                    rigidbody.velocity.y);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rigidbody.velocity = new Vector2(
                    Mathf.Min((rigidbody.velocity.x + Increment), MaxSpeed),
                    rigidbody.velocity.y);
            }
        }
    }
}
