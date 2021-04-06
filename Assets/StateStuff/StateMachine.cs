using UnityEngine;
using System.Collections.Generic;

namespace StateStuff
{
    public class StateMachine
    {
        public Dictionary<StateName, State> states;
        public State currentState;

        public StateMachine()
        {
            states = new Dictionary<StateName, State>();
        }

        public void AddState(State state)
        {
            states.Add(state.Name, state);
        }

        public State GetState(StateName name)
        {
            return states[name];
        }

        public void SetState(StateName name)
        {
            currentState?.Exit();
            currentState = states[name];
            currentState?.Enter();
        }

        public void Update()
        {
            currentState?.Update();
        }

        public void FixedUpdate()
        {
            currentState.FixedUpdate();
        }
    }
}
