using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Player : MonoBehaviour
{
    public float gravity = 1;
    public float jumpSpeed = .3f;
    public float moveSpeed = .3f;

    [SerializeField] Vector2 velocity;

    Controller controller;

    void Start() {
        controller = GetComponent<Controller>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            velocity.y = jumpSpeed;
        }

        velocity.x = Input.GetAxis("Horizontal") * moveSpeed;
    }

    void FixedUpdate() {
        velocity.y -= gravity * Time.deltaTime;
        velocity = controller.Move(velocity);
    }
}
