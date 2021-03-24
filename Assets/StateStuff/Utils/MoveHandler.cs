using UnityEngine;

namespace StateStuff.Utils
{
    public class MoveHandler
    {
        float increment;
        float maxSpeed;

        public float Increment { get { return increment; } }
        public float MaxSpeed { get { return maxSpeed; } }

        public MoveHandler(float increment, float maxSpeed)
        {
            this.increment = increment;
            this.maxSpeed = maxSpeed;
        }

        public void Update(Rigidbody2D rigidbody)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rigidbody.velocity = new Vector2(
                    Mathf.Max((rigidbody.velocity.x - increment), -maxSpeed),
                    rigidbody.velocity.y);
            }
            if (Input.GetKey(KeyCode.D))
            {
                rigidbody.velocity = new Vector2(
                    Mathf.Min((rigidbody.velocity.x + increment), maxSpeed),
                    rigidbody.velocity.y);
            }
        }
    }
}
