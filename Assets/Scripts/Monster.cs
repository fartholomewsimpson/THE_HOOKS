using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(GravityEntity))]
[RequireComponent(typeof(SpriteRenderer))]
public class Monster : MonoBehaviour
{
    public GameObject deathPoofPrefab;
    public float moveSpeed = .1f;
    public bool flip;
    public float strength = 10;

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
        _gravityEntity.AfterGravity += HandleFlip;
        _gravityEntity.Die += Die;
    }

    void HandleFlip() {
        _spriteRenderer.flipX = flip;
        _gravityEntity.velocity.x = flip ? -moveSpeed : moveSpeed;
    }

    void OnWallCollision(RaycastHit2D hit) {
        flip = hit.point.x > transform.position.x;
    }

    void Die() {
        _animator.SetTrigger("Die");

        _gravityEntity.AfterGravity -= HandleFlip;
        _gravityEntity.velocity.x = 0;
    }

    void OnDestroy() {
        GameObject.Instantiate(deathPoofPrefab, transform.position, Quaternion.identity);
    }
}
