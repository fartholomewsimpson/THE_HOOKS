namespace StateStuff
{
    public abstract class State
    {
        protected StateMachine stateMachine;

        public StateName Name { get; private set; }
        public string StateName { get { return Name.ToString(); } }

        public State(StateMachine stateMachine, StateName name) {
            this.stateMachine = stateMachine;
            this.Name = name;
        }

        public virtual void Enter() {}
        public virtual void Update() {}
        public virtual void FixedUpdate() {}
        public virtual void Exit() {}
    }
}
