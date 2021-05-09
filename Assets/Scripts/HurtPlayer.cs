using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtPlayer : MonoBehaviour
{
    public LayerMask playerLayer;
    public int strength = 10;

    Collider2D _collider;

    // TODO: Should this be a separate component? I wanna say yes,
    //        but having the strength defined here feels not right.
    void FixedUpdate() {
        _collider = GetComponent<Collider2D>();
        var player = new Collider2D[1];
        var contactFilter = new ContactFilter2D { layerMask = playerLayer, useLayerMask = true }; // TODO: class level?
        var collisionCount = _collider.OverlapCollider(contactFilter, player);

        if (collisionCount > 0) {
            var entity = player[0].gameObject.GetComponent<GravityEntity>();
            entity.TakeDamage(strength);
        }
    }
}
