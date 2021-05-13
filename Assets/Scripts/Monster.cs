using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(GravityEntity))]
[RequireComponent(typeof(SpriteRenderer))]
public class Monster : MonoBehaviour
{
    public float moveSpeed = .1f;
    public float strength = 10;

    Animator _animator;
    SpriteRenderer _spriteRenderer;
    CollisionHandler _collisionHandler;
    GravityEntity _gravityEntity;

    void Start() {
        _animator = GetComponent<Animator>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetFlip();

        _collisionHandler = GetComponent<CollisionHandler>();
        _collisionHandler.OnHorizontalCollision += OnWallCollision;

        _gravityEntity = GetComponent<GravityEntity>();
        _gravityEntity.Hit += Die;
    }

    void SetFlip() {
        _gravityEntity.velocity.x = _spriteRenderer.flipX ? -moveSpeed : moveSpeed;
    }

    void OnWallCollision(RaycastHit2D hit) {
        _spriteRenderer.flipX = hit.point.x > transform.position.x;

        SetFlip();
    }

    void Die(float amount) {
        _animator.SetTrigger("Die");

        _gravityEntity.velocity.x = 0;
        
        // TODO: Hack. That's another complication with the HurtPlayer script.
        var hurtPlayer = GetComponentInChildren<HurtPlayer>();
        GameObject.DestroyImmediate(hurtPlayer);
        
        GameObject.Destroy(gameObject, 2);
    }
}
