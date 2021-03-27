using UnityEngine;

namespace StateStuff
{
    public abstract class State
    {
        public abstract State Update(Rigidbody2D rigidbody);
        public virtual void HandleCollision(Collision2D collision) {}
        public StateName Name { get; private set; }
        public string DisplayName { get { return Name.ToString(); } }

        public State(StateName stateName)
        {
            Name = stateName;
        }
    }
}
