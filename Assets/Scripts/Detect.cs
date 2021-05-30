using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Detect : MonoBehaviour
{
    public LayerMask layerMask;

    Collider2D _lineOfSight;
    [SerializeField] bool _examining;
    [SerializeField] int _maxVisible = 10;
    [SerializeField] Collider2D[] _visible;

    void Start() {
        _lineOfSight = GetComponent<Collider2D>();
    }

    public void Examine(InputAction.CallbackContext context) {
        if (context.ReadValueAsButton() && _examining) {
            _examining = true;
            var newlyVisible = new Collider2D[_maxVisible];
            var contactFilter = new ContactFilter2D{layerMask=layerMask, useLayerMask=true};
            if (_lineOfSight.OverlapCollider(contactFilter, newlyVisible) > 0) {
                for (int i = 0; i < _visible.Length; i++) {
                    if (!newlyVisible.Contains(_visible[i])) {
                        
                    }
                }
            }
        } else if(!context.ReadValueAsButton()) {
            _examining = false;
        }
    }
}
