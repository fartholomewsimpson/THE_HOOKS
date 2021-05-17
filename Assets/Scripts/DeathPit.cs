using UnityEngine;
using Utils;

[RequireComponent(typeof(Collider2D))]
public class DeathPit : MonoBehaviour
{
    public LayerMask playerMask;
    public LayerMask monsterMask;

    new Collider2D collider;

    LayerMask _layerMask;

    void Start() {
        collider = GetComponent<Collider2D>();

        _layerMask = playerMask | monsterMask;
    }

    void FixedUpdate() {
        var colliders = new Collider2D[1];
        var contactFilter = new ContactFilter2D{layerMask = _layerMask, useLayerMask = true};
        var collisionNumber = Physics2D.OverlapCollider(collider, contactFilter, colliders);
        if (collisionNumber > 0) {
            foreach (var col in colliders) {
                Debug.Log(col.name);
                var entity = col.gameObject.GetComponent<GravityEntity>();
                if (entity != null) {
                    entity.TakeDamage(float.MaxValue);
                }
            }
        }
    }
}
