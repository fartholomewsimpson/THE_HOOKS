using UnityEngine;
using UnityEngine.UI;

namespace StateStuff
{
    [RequireComponent(typeof(Text))]
    public class DisplayStateName : MonoBehaviour
    {
        public StateMachine stateMachine;

        private Text display;

        void Start()
        {
            display = GetComponent<Text>();
        }

        void Update()
        {
            display.text = stateMachine.CurrentState.DisplayName;
        }
    }
}
