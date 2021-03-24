using UnityEngine;

namespace StateStuff
{
    public abstract class State
    {
        public enum StateName { STANDING, WALKING, JUMPING }
        public abstract State Update(Rigidbody2D rigidbody);

        protected StateName name;
        public StateName Name { get { return name; } }
        public string DisplayName { get { return name.ToString(); } }

        public State(StateName stateName)
        {
            this.name = stateName;
        }
    }
}
