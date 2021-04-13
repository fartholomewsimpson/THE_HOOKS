using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller : MonoBehaviour
{
    [Range(2, 10)]
    public int rayCount = 4;

    float paddingWidth = .05f;
    new BoxCollider2D collider;
    Bounds bounds;
    Vector2 topLeft, topRight, bottomLeft, bottomRight;

    public void Move(Vector2 velocity)
    {
        CalculateCorners(); // TODO: Where should this even go?
        DetectVerticalCollisions(velocity);
        transform.Translate(velocity);
    }

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    void CalculateCorners()
    {
        bounds = collider.bounds;
        bounds.Expand(-paddingWidth);

        topLeft = new Vector2(bounds.min.x, bounds.max.y);
        topRight = new Vector2(bounds.max.x, bounds.max.y);
        bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        bottomRight = new Vector2(bounds.max.x, bounds.min.y);
    }

    void DetectVerticalCollisions(Vector2 velocity)
    {
        var spacing = (bottomRight.x - bottomLeft.x) / (rayCount - 1);
        for (int i = 0; i < rayCount; i++)
        {
            var start = new Vector2(bounds.min.x + (spacing * i), bounds.min.y);
            var direction = Vector2.up * Mathf.Sign(velocity.y);
            var distance = Mathf.Abs(velocity.y) + paddingWidth;
            Debug.DrawRay(start, direction * distance, Color.red);
            var hit = Physics2D.Raycast(start, direction, distance);
            if (hit)
                Debug.Log($"Hit: {hit}");
        }
    }
}
