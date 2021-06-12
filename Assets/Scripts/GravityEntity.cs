using System;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public class GravityEntity : MonoBehaviour
{
    public Vector2 velocity;
    public float gravity = 1;
    public float health;

    public event Action BeforeGravity, AfterGravity;
    public event Action<float, Vector2> Hit;
    public event Action Die;

    CollisionHandler _collisionHandler;

    void Start() {
        _collisionHandler = GetComponent<CollisionHandler>();
        _collisionHandler.OnVerticalCollision += HandleVerticalCollision;
    }

    void FixedUpdate() {
        if (BeforeGravity != null)
            BeforeGravity();
        velocity.y -= gravity * Time.deltaTime;
        if (AfterGravity != null)
            AfterGravity();

        velocity = _collisionHandler.Move(velocity);
    }

    void HandleVerticalCollision(RaycastHit2D hit) {
        velocity.y = 0;
    }

    public void TakeDamage(float amount, Vector2 direction = default) {
        if (Hit != null)
            Hit(amount, direction);
        
        health = Mathf.Max(health - amount, 0);

        if (health <= 0) {
            if (Die != null) {
                Die();
            }
        }
    }
}
