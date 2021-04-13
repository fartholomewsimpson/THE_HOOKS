using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Player : MonoBehaviour
{
    public float gravity = 1;

    Vector2 velocity;

    Controller controller;

    void Start()
    {
        controller = GetComponent<Controller>();
    }

    void Update()
    {
        velocity.y = (velocity.y - gravity) * Time.deltaTime;

        controller.Move(velocity);
    }
}
