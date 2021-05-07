using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(GravityEntity))]
[RequireComponent(typeof(SpriteRenderer))]
public class Monster : MonoBehaviour
{
    public float moveSpeed = .1f;
    public bool flip;

    Animator _animator;
    SpriteRenderer _spriteRenderer;
    CollisionHandler _collisionHandler;
    GravityEntity _gravityEntity;

    void Start() {
        _animator = GetComponent<Animator>();

        _spriteRenderer = GetComponent<SpriteRenderer>();

        _collisionHandler = GetComponent<CollisionHandler>();
        _collisionHandler.OnHorizontalCollision += OnWallCollision;

        _gravityEntity = GetComponent<GravityEntity>();
        _gravityEntity.AfterGravity += DoThing;
        _gravityEntity.Hit += Die;
    }

    void DoThing() {
        _spriteRenderer.flipX = flip;
        _gravityEntity.velocity.x = flip ? -moveSpeed : moveSpeed;
    }

    void OnWallCollision(RaycastHit2D hit) {
        flip = hit.point.x > transform.position.x;
    }

    void Die() {
        _gravityEntity.AfterGravity -= DoThing;
        _gravityEntity.velocity.x = 0;
        _animator.SetTrigger("Die");
        GameObject.Destroy(gameObject, 2);
    }
}
