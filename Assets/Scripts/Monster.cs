using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(GravityEntity))]
[RequireComponent(typeof(SpriteRenderer))]
public class Monster : MonoBehaviour
{
    public GameObject deathPoofPrefab;
    public float moveSpeed = .1f;
    public float strength = 10;
    public Collider2D hurtBox;
    public ContactFilter2D contactFilter;

    Animator _animator;
    SpriteRenderer _spriteRenderer;
    CollisionHandler _collisionHandler;
    GravityEntity _gravityEntity;

    float MoveSpeed {
        get { return _spriteRenderer.flipX ? -moveSpeed : moveSpeed; }
    }

    void Start() {
        _animator = GetComponent<Animator>();

        _spriteRenderer = GetComponent<SpriteRenderer>();

        _collisionHandler = GetComponent<CollisionHandler>();
        _collisionHandler.OnHorizontalCollision += OnWallCollision;

        _gravityEntity = GetComponent<GravityEntity>();
        _gravityEntity.Die += Die;

        _gravityEntity.velocity.x = MoveSpeed;
    }

    void FixedUpdate() {
        _gravityEntity.velocity.x = MoveSpeed;
        var hits = new Collider2D[5];
        Physics2D.OverlapCollider(hurtBox, contactFilter, hits);
        foreach (var hit in hits) {
            var entity = hit?.GetComponent<GravityEntity>();
            if (entity != null && entity.gameObject != this.gameObject) {
                var direction = (Vector2)(entity.transform.position) - hit.ClosestPoint(transform.position);
                entity.TakeDamage(strength, direction);
            }
        }
    }

    void OnWallCollision(RaycastHit2D hit) {
        _spriteRenderer.flipX = hit.point.x > transform.position.x;
    }

    void Die() {
        _animator.SetTrigger("Die");
        _gravityEntity.velocity.x = 0;

        GameObject.Instantiate(deathPoofPrefab, transform.position, Quaternion.identity);
    }
}
