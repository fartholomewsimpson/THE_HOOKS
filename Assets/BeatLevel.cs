using UnityEngine;

public class BeatLevel : MonoBehaviour
{
    public LayerMask layerMask;

    new BoxCollider2D collider;

    void Start() {
        collider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate() {
        var overlaps = Physics2D.OverlapBox(collider.bounds.center, collider.bounds.extents, 0, layerMask);
        if (overlaps != null) {
            Debug.Log("WINNER");
        }
    }
}
