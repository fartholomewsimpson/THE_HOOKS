using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Player : MonoBehaviour
{
    public float gravity = .5f;

    Vector2 velocity;

    Controller controller;

    void Start()
    {
        controller = GetComponent<Controller>();
    }

    void Update()
    {
        velocity.y -= gravity;

        controller.Move(velocity * Time.deltaTime);
    }
}
