using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(GravityEntity))]
[RequireComponent(typeof(SpriteRenderer))]
public class Monster : MonoBehaviour
{
    public Transform lineOfSight;
    public float moveSpeed = .1f;
    public float strength = 10;
    public Collider2D hurtBox;
    public ContactFilter2D contactFilter;

    [SerializeField] bool _flipped;
    [SerializeField] bool _dead;
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

    void Update() {
        if (_spriteRenderer.flipX != _flipped) {
            _flipped = _spriteRenderer.flipX;
            lineOfSight.localScale = new Vector3(
                lineOfSight.localScale.x * -1,
                lineOfSight.localScale.y,
                lineOfSight.localScale.z);
        }
    }

    void FixedUpdate() {
        _gravityEntity.velocity.x = _dead ? 0 : MoveSpeed;
        var hits = new Collider2D[5];
    }

    void OnWallCollision(RaycastHit2D hit) {
        // TODO: This will be handled by the neural network for behavior maybe
        _spriteRenderer.flipX = hit.point.x > transform.position.x;
    }

    void Die() {
        _animator.SetTrigger("Die");
        _dead = true;
    }
}
