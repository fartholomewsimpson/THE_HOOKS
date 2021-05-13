using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtPlayer : MonoBehaviour
{
    public LayerMask playerLayer;
    public int strength = 10;

    Collider2D _collider;

    // TODO: Can't get point of contact from collision due to nature of collision detection here.
    //       Does it make sense to have this component, or could this be handled elsewhere?
    void FixedUpdate() {
        _collider = GetComponent<Collider2D>();
        var player = new Collider2D[1];
        // TODO: class level? Always is the same afterall.
        var contactFilter = new ContactFilter2D { layerMask = playerLayer, useLayerMask = true };
        var collisionCount = _collider.OverlapCollider(contactFilter, player);

        if (collisionCount > 0) {
            var entity = player[0].gameObject.GetComponent<GravityEntity>();
            entity.TakeDamage(strength);
        }
    }
}
