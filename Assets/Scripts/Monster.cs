using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public class Monster : MonoBehaviour
{
    CollisionHandler _controller;

    [SerializeField] float _velocity;

    void Start() {
        _controller = GetComponent<CollisionHandler>();
    }

    void FixedUpdate() {
        _controller.Move(new Vector2(_velocity, 0));
    }
}
