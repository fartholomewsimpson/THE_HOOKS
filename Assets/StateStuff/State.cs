using UnityEngine;

namespace StateStuff
{
    public abstract class State
    {
        public abstract State Update(Rigidbody2D rigidbody);
        public virtual void HandleCollision(Collision2D collision) {}
        public StateName Name { get { return name; } }
        public string DisplayName { get { return name.ToString(); } }
        protected StateName name;

        public State(StateName stateName)
        {
            this.name = stateName;
        }
    }
}
