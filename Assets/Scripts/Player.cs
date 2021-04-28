using StateStuff;
using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Player : MonoBehaviour
{
    public float gravity = 1;
    public float jumpSpeed = .3f;
    public int maxJumpAmount = 10;
    public float moveSpeed = .3f;
    public float moveIncrement = .01f;

    [SerializeField] Vector2 velocity;
    bool canJump;
    bool jumping;
    int jumpCounter;

    Controller controller;

    void Start() {
        controller = GetComponent<Controller>();
        controller.onVerticalCollision += handleVerticalCollision;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && canJump) {
            jumping = true;
            canJump = false;
        }
    }

    void FixedUpdate() {
        velocity.y -= gravity * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) {
            velocity.x = Mathf.Max(-moveSpeed, velocity.x - moveIncrement);
        } else if (Input.GetKey(KeyCode.D)) {
            velocity.x = Mathf.Min(moveSpeed, velocity.x + moveIncrement);
        } else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
            if (Mathf.Abs(velocity.x) < moveIncrement)
                velocity.x = 0;
            else
                velocity.x += Mathf.Sign(velocity.x) * -moveIncrement;
        }

        if (jumping && Input.GetKey(KeyCode.Space)) {
            velocity.y = jumpSpeed;
            jumpCounter++;
            if (jumpCounter >= maxJumpAmount) {
                jumping = false;
                jumpCounter = 0;
            }
        } else {
            jumping = false;
            jumpCounter = 0;
        }

        velocity = controller.Move(velocity);
    }

    void handleVerticalCollision(RaycastHit2D hit)
    {
        if (hit.point.y > transform.position.y) {
            jumping = false;
        } else {
            canJump = true;
        }
    }
}
