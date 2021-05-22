using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(GravityEntity))]
public class Frog : MonoBehaviour
{
    public Collider2D lineOfSight;
    public Animator animator;
    public new ParticleSystem particleSystem;
    public LayerMask visionLayer;
    public float moveSpeed = .5f;
    public float aggroZone = 10;
    public float dangerZone = .5f;

    [SerializeField] bool flip;

    SpriteRenderer _spriteRenderer;
    CollisionHandler _collisionHandler;
    GravityEntity _gravityEntity;

    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _collisionHandler = GetComponent<CollisionHandler>();
        _collisionHandler.OnHorizontalCollision += OnWallCollision;
        _collisionHandler.OnVerticalCollision += OnVerticalCollision;
        
        _gravityEntity = GetComponent<GravityEntity>();
        _gravityEntity.AfterGravity += HandleUpdate;
        _gravityEntity.Die += HandleDeath;
    }

    void OnWallCollision(RaycastHit2D hit) {
        flip = hit.point.x > transform.position.x;
    }

    void OnVerticalCollision(RaycastHit2D hit) {
        if (hit.point.y < transform.position.y) {
            _gravityEntity.velocity.x = 0;
        } else {
            // TODO: Where should damage come from?
            _gravityEntity.TakeDamage(100);
        }
    }

    void HandleUpdate() {
        _spriteRenderer.flipX = flip;

        if (lineOfSight != null) {
            var visible = new Collider2D[10];
            var contactFilter = new ContactFilter2D{ layerMask = visionLayer, useLayerMask = true };
            if (lineOfSight.OverlapCollider(contactFilter, visible) > 0) {
                int closestIndex = 0;
                float closestDistance = float.MaxValue;
                for (int i = 0; i < visible.Length && visible[i] != null; i++) {
                    var distance = (transform.position - visible[i].transform.position).magnitude;
                    if (distance < closestDistance) {
                        closestDistance = distance;
                        closestIndex = i;
                    }
                }

                if (closestDistance < dangerZone) {
                    Debug.Log("RUN AWAY");
                } else if (closestDistance < aggroZone) {
                    Debug.Log("AGGRO");
                }
            }
        }
    }

    void HandleDeath() {
        if (particleSystem != null) {
            particleSystem.Play();
        }
    }
}
