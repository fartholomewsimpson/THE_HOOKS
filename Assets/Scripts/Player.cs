using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(GravityEntity))]
public class Player : MonoBehaviour
// TODO: Right now this class does everything. What should this class do, and where should the rest go?
{
    public float jumpSpeed = .2f;
    public int maxJumpAmount = 12;
    public float moveSpeed = .2f;
    public float moveIncrement = .015f;
    public string statusTag = "status";

    [SerializeField] bool _grounded;
    [SerializeField] bool _canJump;
    [SerializeField] bool _jumping;
    [SerializeField] int _jumpCounter;
    public bool aiming;
    public bool downward;

    // TODO: Move animation elsewhere?
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
        _gravityEntity.Hit += DamagePlayer;
        _gravityEntity.Die += OnDeath;
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

        if (_jumpInput && _grounded && _canJump) {
            _jumping = true;
            _grounded = false;
            _canJump = false;
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

            if (!_jumpInput)
                _canJump = true;
        }
    }

    void DamagePlayer(float power, Vector2 direction) {
        _animator.SetTrigger("GetHurt");

        _gravityEntity.velocity = .15f * direction;
    }

    void OnDeath() {
        _animator.SetTrigger("Die");

        _gravityEntity.velocity = Vector2.zero;
        _gravityEntity.Hit -= DamagePlayer;
        _collisionHandler.OnVerticalCollision -= HandleVerticalCollision;
        this.enabled = false;

        StartCoroutine(Reset());
    }

    public void Move(InputAction.CallbackContext context) => _moveInput = context.ReadValue<float>();
    public void Jump(InputAction.CallbackContext context) {
        _jumpInput = context.ReadValueAsButton();
    }

    public void Aim(InputAction.CallbackContext context) {
        aiming = context.ReadValueAsButton();
        _animator.SetBool("Aiming", aiming);
    }
    
    public void LookDown(InputAction.CallbackContext context) {
        downward = context.ReadValueAsButton();
        _animator.SetBool("Downward", downward);
    }

    // TODO: This is a hack. Move this to somewhere where it makes more sense.
    IEnumerator Reset() {
        yield return new WaitForSeconds(3);

        var scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
