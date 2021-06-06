using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleStatuses : MonoBehaviour
{
    public string statusTag;
    [SerializeField] bool _activated;

    public void Toggle(InputAction.CallbackContext context) {
        if (context.ReadValueAsButton() && context.started) {
            _activated = !_activated;

            Debug.Log($"statuses: {_activated}");
            var objs = GameObject.FindGameObjectsWithTag(statusTag);
            foreach (var obj in objs) {
                var spriteRenderer = obj.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null) {
                    spriteRenderer.enabled = _activated;
                }
                var canvas = obj.GetComponent<Canvas>();
                if (canvas != null) {
                    canvas.enabled = _activated;
                }
            }
        }
    }
}
