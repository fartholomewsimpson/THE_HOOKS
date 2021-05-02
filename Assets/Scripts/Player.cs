using StateStuff;
using UnityEngine;

[RequireComponent(typeof(Controller))]
[RequireComponent(typeof(PlayerData))]
public class Player : MonoBehaviour
{
    public PlayerData playerData;

    public float gravity;
    public float jumpSpeed;
    public int maxJumpAmount;
    public float moveSpeed;
    public float moveIncrement;

    [SerializeField] bool _canJump;
    [SerializeField] bool _jumping;
    [SerializeField] int _jumpCounter;

    Controller controller;

    void Start() {
        controller = GetComponent<Controller>();

        controller.onVerticalCollision += handleVerticalCollision;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && _canJump) {
            _jumping = true;
            _canJump = false;
        }
    }

    void FixedUpdate() {
        playerData.velocity.y -= gravity * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) {
            playerData.velocity.x = Mathf.Max(-moveSpeed, playerData.velocity.x - moveIncrement);
        } else if (Input.GetKey(KeyCode.D)) {
            playerData.velocity.x = Mathf.Min(moveSpeed, playerData.velocity.x + moveIncrement);
        } else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
            if (Mathf.Abs(playerData.velocity.x) < moveIncrement)
                playerData.velocity.x = 0;
            else
                playerData.velocity.x += Mathf.Sign(playerData.velocity.x) * -moveIncrement;
        }

        if (_jumping && Input.GetKey(KeyCode.Space)) {
            playerData.velocity.y = jumpSpeed;
            _jumpCounter++;
            if (_jumpCounter >= maxJumpAmount) {
                _jumping = false;
            }
        } else {
            _jumping = false;
        }
        
        playerData.velocity = controller.Move(playerData.velocity);
    }

    void handleVerticalCollision(RaycastHit2D hit)
    {
        if (hit.point.y > transform.position.y) {
            _jumping = false;
        } else {
            _canJump = true;
            _jumpCounter = 0;
        }
        playerData.velocity.y = 0;
    }
}
