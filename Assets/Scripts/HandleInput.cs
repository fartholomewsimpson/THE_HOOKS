using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(DisplayEntityInfo))]
public class HandleInput : MonoBehaviour
{
    DisplayEntityInfo _display;

    void Start() {
        _display = GetComponent<DisplayEntityInfo>();
    }
    
    public void ToggleStatuses(InputAction.CallbackContext context) {
        if (context.ReadValueAsButton() && context.started) {
            _display.ToggleStatuses();
        }
    }
}
