using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtPlayer : MonoBehaviour
{
    public LayerMask playerLayer;

    Collider2D _collider;

    void Start() {
        _collider = GetComponent<Collider2D>();
    }

    void FixedUpdate() {
        var player = new Collider2D[10];
        var contactFilter = new ContactFilter2D { layerMask = playerLayer, useLayerMask = true };
        if (_collider.OverlapCollider(contactFilter, player) > 0) {
            Debug.Log("DEATH BECOMES YOU");
        }
    }
}
