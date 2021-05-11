using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(GravityEntity))]
public class Player : MonoBehaviour
{
    public float jumpSpeed = .2f;
    public int maxJumpAmount = 12;
    public float moveSpeed = .2f;
    public float moveIncrement = .015f;

    [SerializeField] bool _grounded;
    [SerializeField] bool _jumping;
    [SerializeField] int _jumpCounter;
    [SerializeField] float _health = 100;

    // TODO: Move animation elsewhere
    Animator _animator;
    CollisionHandler _collisionHandler;
    GravityEntity _gravityEntity;
    float _moveInput;
    bool _jumpInput;

    void Start() {
        _animator = GetComponentInChildren<Animator>();

        _collisionHandler = GetComponent<CollisionHandler>();
        _collisionHandler.OnVerticalCollision += HandleVerticalCollision;

        _gravityEntity = GetComponent<GravityEntity>();
        // _gravityEntity.AfterGravity += HandleInput;
        _gravityEntity.Hit += DamagePlayer;
    }

    void FixedUpdate() {
        if (_moveInput < 0) {
            _gravityEntity.velocity.x = Mathf.Max(-moveSpeed, _gravityEntity.velocity.x - moveIncrement);
        } else if (_moveInput > 0) {
            _gravityEntity.velocity.x = Mathf.Min(moveSpeed, _gravityEntity.velocity.x + moveIncrement);
        } else {
            if (Mathf.Abs(_gravityEntity.velocity.x) <= moveIncrement)
                _gravityEntity.velocity.x = 0;
            else
                _gravityEntity.velocity.x += Mathf.Sign(_gravityEntity.velocity.x) * -moveIncrement;
        }

        if (_jumpInput && _grounded) {
            _jumping = true;
            _grounded = false;
        }

        if (_jumping && _jumpInput) {
            _gravityEntity.velocity.y = jumpSpeed;
            _jumpCounter++;
            if (_jumpCounter >= maxJumpAmount) {
                _jumping = false;
            }
        } else {
            _jumping = false;
        }

        if (_gravityEntity.velocity.y < 0) {
            _grounded = false;
        }
    }

    void HandleVerticalCollision(RaycastHit2D hit) {
        if (hit.point.y > transform.position.y) {
            _jumping = false;
        } else {
            _grounded = true;
            _jumpCounter = 0;
        }
    }

    void DamagePlayer(float power) {
        _animator.SetTrigger("GetHurt");

        _health = Mathf.Max(_health - power, 0);

        // TODO: replace with directional velocity based on point of impact
        _gravityEntity.velocity = new Vector2(-.15f, .15f);

        // TODO: Display health
        Debug.Log($"Health: {_health}");

        if (_health <= 0) {
            _animator.SetTrigger("Die");

            _gravityEntity.velocity = Vector2.zero;
            _gravityEntity.Hit -= DamagePlayer;
            _collisionHandler.OnVerticalCollision -= HandleVerticalCollision;
            this.enabled = false;
        }
    }

    public void Move(InputAction.CallbackContext context) => _moveInput = context.ReadValue<float>();
    public void Jump(InputAction.CallbackContext context) {
        _jumpInput = context.ReadValueAsButton();
    }
}
