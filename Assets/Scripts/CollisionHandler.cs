using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CollisionHandler : MonoBehaviour
{
    [Range(2, 10)]
    public int rayCount = 4;
    public LayerMask collisionMask = 1;
    public float paddingWidth = .05f;

    public event Action<RaycastHit2D> OnVerticalCollision, OnHorizontalCollision;

    new BoxCollider2D collider;
    Bounds bounds;
    Vector2 topLeft, topRight, bottomLeft, bottomRight;

    struct CornerPair {
        public Vector2 start;
        public Vector2 end;
    }

    // TODO: Rather than having a public move function, maybe this should be a component and just move on fixedupdate?
    public Vector2 Move(Vector2 velocity) {
        CalculateCorners();
        if (Mathf.Abs(velocity.y) > 0)
            DetectVerticalCollisions(ref velocity);
        if (Mathf.Abs(velocity.x) > 0)
            DetectHorizontalCollisions(ref velocity);
        transform.Translate(velocity);
        return velocity;
    }

    void Start() {
        collider = GetComponent<BoxCollider2D>();
    }

    void CalculateCorners() {
        bounds = collider.bounds;
        bounds.Expand(-paddingWidth * 2);

        topLeft = new Vector2(bounds.min.x, bounds.max.y);
        topRight = new Vector2(bounds.max.x, bounds.max.y);
        bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        bottomRight = new Vector2(bounds.max.x, bounds.min.y);

        Debug.DrawLine(topLeft, topRight);
        Debug.DrawLine(topRight, bottomRight);
        Debug.DrawLine(bottomRight, bottomLeft);
        Debug.DrawLine(bottomLeft, topLeft);
    }

    // TODO: Break out core logic
    void DetectVerticalCollisions(ref Vector2 velocity) {
        var sign = Mathf.Sign(velocity.y);
        var pair = sign > 0
            ? new CornerPair { start = topLeft, end = topRight }
            : new CornerPair { start = bottomLeft, end = bottomRight };
        var spacing = (pair.end.x - pair.start.x) / (rayCount - 1);
        var direction = Vector2.up * sign;
        var distance = Mathf.Abs(velocity.y) + paddingWidth;

        for (int i = 0; i < rayCount; i++) {
            var origin = new Vector2(pair.start.x + (spacing * i), pair.start.y);
            Debug.DrawRay(origin, direction * distance, Color.red);

            var hit = Physics2D.Raycast(origin, direction, distance, collisionMask);
            if (hit) {
                velocity.y = (hit.distance - paddingWidth) * sign;
                Debug.DrawRay(origin, direction * distance, Color.blue);
                distance = hit.distance;
                if (OnVerticalCollision != null)
                    OnVerticalCollision(hit);
            }
        }
    }

    void DetectHorizontalCollisions(ref Vector2 velocity) {
        var sign = Mathf.Sign(velocity.x);
        var pair = sign > 0
            ? new CornerPair { start = bottomRight, end = topRight }
            : new CornerPair { start = bottomLeft, end = topLeft };
        var spacing = (pair.end.y - pair.start.y) / (rayCount - 1);
        var direction = Vector2.right * sign;
        var distance = Mathf.Abs(velocity.x) + paddingWidth;

        for (int i = 0; i < rayCount; i++) {
            // velocity.y added here since horizontal is calculated 2nd. This is to fix the corner cliping problem
            var origin = new Vector2(pair.start.x, pair.start.y + (spacing * i) + velocity.y);
            Debug.DrawRay(origin, direction * distance, Color.red);

            var hit = Physics2D.Raycast(origin, direction, distance, collisionMask);
            if (hit) {
                velocity.x = (hit.distance - paddingWidth) * sign;
                Debug.DrawRay(origin, direction * distance, Color.blue);
                distance = hit.distance;
                if (OnHorizontalCollision != null)
                    OnHorizontalCollision(hit);
            }
        }
    }
}
