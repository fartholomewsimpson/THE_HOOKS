using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(GravityEntity))]
public class Player : MonoBehaviour
{
    public float jumpSpeed = .2f;
    public int maxJumpAmount = 12;
    public float moveSpeed = .2f;
    public float moveIncrement = .015f;

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
            _gravityEntity.velocity.x = Mathf.Max(-moveSpeed, _gravityEntity.velocity.x - moveIncrement);
        } else if (Input.GetKey(KeyCode.D)) {
            _gravityEntity.velocity.x = Mathf.Min(moveSpeed, _gravityEntity.velocity.x + moveIncrement);
        } else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
            if (Mathf.Abs(_gravityEntity.velocity.x) < moveIncrement)
                _gravityEntity.velocity.x = 0;
            else
                _gravityEntity.velocity.x += Mathf.Sign(_gravityEntity.velocity.x) * -moveIncrement;
        }

        if (_jumping && Input.GetKey(KeyCode.Space)) {
            _gravityEntity.velocity.y = jumpSpeed;
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
