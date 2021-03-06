using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(GravityEntity))]
public class Frog : MonoBehaviour
{
    public GameObject tonguePrefab;
    public Collider2D lineOfSight;
    public Animator animator;
    public LayerMask visionLayer;
    public LayerMask tongueLayer;
    public float moveSpeed = .5f;
    public float aggroZone = 3;
    public float dangerZone = 1f;
    public float strength = 10;
    public float tongueWidth = .3f;
    public float tongueLength = 5;

    [SerializeField] bool _flip;
    [SerializeField] bool _jumping;
    [SerializeField] bool _shootingTongue;
    [SerializeField] bool _flipped;

    CollisionHandler _collisionHandler;
    GravityEntity _gravityEntity;
    GameObject _tongue;

    void Start() {
        _collisionHandler = GetComponent<CollisionHandler>();
        _collisionHandler.OnVerticalCollision += OnVerticalCollision;
        
        _gravityEntity = GetComponent<GravityEntity>();
        _gravityEntity.AfterGravity += HandleUpdate;
        _gravityEntity.Die += Die;
    }

    void Update() {
        if (_flip != _flipped) {
            _flipped = _flip;
            lineOfSight.transform.localScale = new Vector3(
                lineOfSight.transform.localScale.x * -1,
                lineOfSight.transform.localScale.y,
                lineOfSight.transform.localScale.z);
        }
    }

    void OnVerticalCollision(RaycastHit2D hit) {
        if (hit.point.y < transform.position.y) {
            _gravityEntity.velocity.x = 0;
            _jumping = false;
        }
    }

    void HandleUpdate() {
        if (!_jumping && lineOfSight == null)
            return;

        var visible = new Collider2D[10];
        var contactFilter = new ContactFilter2D{ layerMask = visionLayer, useLayerMask = true };
        if (lineOfSight.OverlapCollider(contactFilter, visible) > 0) {
            int closestIndex = 0;
            float closestDistance = float.MaxValue;
            for (int i = 0; i < visible.Length && visible[i] != null; i++) {
                if (visible[i].gameObject.name != this.gameObject.name) {
                    var distance = (transform.position - visible[i].transform.position).magnitude;
                    if (distance < closestDistance) {
                        closestDistance = distance;
                        closestIndex = i;
                    }
                }
            }

            // TODO: this should be removed and replaced with the neural network
            // if (closestDistance <= dangerZone) {
            //     Debug.DrawRay(transform.position, visible[closestIndex].transform.position - transform.position, Color.green, 2);

            //     _jumping = true;
            //     _flip = visible[closestIndex].transform.position.x > transform.position.x;
            //     _spriteRenderer.flipX = _flip;
            //     _gravityEntity.velocity.x = _flip ? -moveSpeed : moveSpeed;
            //     _gravityEntity.velocity.y = moveSpeed;
            // } else if (closestDistance <= aggroZone) {
            //     Debug.DrawRay(transform.position, visible[closestIndex].transform.position - transform.position, Color.green, 2);
            //     ShootTongue(visible[closestIndex]);
            // }
        }
    }

    void ShootTongue(Collider2D collider) {
        if (!_shootingTongue && _tongue == null) {
            animator.SetTrigger("Tongue");
            _shootingTongue = true;
            
            var target = collider.transform.position;
            var direction = (target - transform.position).normalized;
            var hits = Physics2D.RaycastAll(transform.position, direction, tongueLength, tongueLayer);
            foreach (var hit in hits.Where(h => h.collider.gameObject != this.gameObject)) {
                if (hit.collider == collider) {
                    var entity = hit.collider.GetComponent<GravityEntity>();
                    if (entity != null) {
                        entity.TakeDamage(strength, direction);
                    }
                }
            }

            _tongue = GameObject.Instantiate(
                tonguePrefab,
                transform.position,
                Quaternion.LookRotation(Vector3.forward, direction),
                transform);
            var lineRenderer = _tongue.GetComponent<LineRenderer>();
            lineRenderer.startWidth = tongueWidth;
            lineRenderer.endWidth = tongueWidth;
            lineRenderer.SetPositions(new Vector3[] { transform.position, target });
        } else {
            _shootingTongue = false;
            GameObject.Destroy(_tongue, 1);
        }
    }

    void Die() {
        _gravityEntity.AfterGravity -= HandleUpdate;
    }
}
