using UnityEngine;

namespace StateStuff
{
    /// <summary>
    /// Classes responsible for logic around handling inputs.
    /// </summary>
    public abstract class ControlState
    {
        public abstract ControlState Update(Vector2 velocity);
        public virtual void FixedUpdate(Rigidbody2D rigidbody) {}
        public virtual void HandleCollision(Collision2D collision) {}
        public StateName Name { get; private set; }
        public string DisplayName { get { return Name.ToString(); } }

        public ControlState(StateName stateName)
        {
            Name = stateName;
        }
    }
}
