using System;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(EntityData))]
public class GravityEntity : MonoBehaviour
{
    public EntityData entityData;
    public float gravity;

    public event Action BeforeGravity, AfterGravity;

    [SerializeField] Vector2 _velocity;
    CollisionHandler _collisionHandler;

    void Start() {
        _collisionHandler = GetComponent<CollisionHandler>();
        _collisionHandler.OnVerticalCollision += HandleVerticalCollision;
    }

    void FixedUpdate() {
        if (BeforeGravity != null)
            BeforeGravity();
        entityData.velocity.y -= gravity * Time.deltaTime;
        if (AfterGravity != null)
            AfterGravity();

        entityData.velocity = _collisionHandler.Move(entityData.velocity);
    }

    void HandleVerticalCollision(RaycastHit2D hit) {
        entityData.velocity.y = 0;
    }
}
