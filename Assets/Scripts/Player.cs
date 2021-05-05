using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(GravityEntity))]
[RequireComponent(typeof(EntityData))]
public class Player : MonoBehaviour
{
    public EntityData entityData;
    public float jumpSpeed;
    public int maxJumpAmount;
    public float moveSpeed;
    public float moveIncrement;

    [SerializeField] bool _canJump;
    [SerializeField] bool _jumping;
    [SerializeField] int _jumpCounter;

    CollisionHandler _collisionHandler;
    GravityEntity _gravityEntity;

    void Start() {
        _collisionHandler = GetComponent<CollisionHandler>();
        _collisionHandler.OnVerticalCollision += HandleVerticalCollision;

        _gravityEntity = GetComponent<GravityEntity>();
        _gravityEntity.AfterGravity += HandleInput;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && _canJump) {
            _jumping = true;
            _canJump = false;
        }
    }

    void HandleInput() {
        if (Input.GetKey(KeyCode.A)) {
            entityData.velocity.x = Mathf.Max(-moveSpeed, entityData.velocity.x - moveIncrement);
        } else if (Input.GetKey(KeyCode.D)) {
            entityData.velocity.x = Mathf.Min(moveSpeed, entityData.velocity.x + moveIncrement);
        } else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
            if (Mathf.Abs(entityData.velocity.x) < moveIncrement)
                entityData.velocity.x = 0;
            else
                entityData.velocity.x += Mathf.Sign(entityData.velocity.x) * -moveIncrement;
        }

        if (_jumping && Input.GetKey(KeyCode.Space)) {
            entityData.velocity.y = jumpSpeed;
            _jumpCounter++;
            if (_jumpCounter >= maxJumpAmount) {
                _jumping = false;
            }
        } else {
            _jumping = false;
        }
    }

    void HandleVerticalCollision(RaycastHit2D hit) {
        if (hit.point.y > transform.position.y) {
            _jumping = false;
        } else {
            _canJump = true;
            _jumpCounter = 0;
        }
    }
}
