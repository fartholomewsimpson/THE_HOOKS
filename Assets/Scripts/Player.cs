using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Player : MonoBehaviour
{
    public float gravity = 1;

    [SerializeField] Vector2 velocity;

    Controller controller;

    void Start() {
        controller = GetComponent<Controller>();
    }

    void FixedUpdate() {
        velocity.y -= gravity * Time.deltaTime;
        velocity = controller.Move(velocity);
    }
}
