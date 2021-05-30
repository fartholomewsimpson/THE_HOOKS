using Relationships;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEntityInfo : MonoBehaviour
{
    // relationship to player is common among most entities
    public RelationshipEntity playerEntity;
    public RelationshipEntity relationshipEntity;

    Text text;

    void Start() {
        text = GetComponent<Text>();
    }

    void Update() {
        var rating = relationshipEntity.GetEntityRating(playerEntity);
        if (rating > 0) {
            text.color = Color.green;
        } else if (rating < 0) {
            text.color = Color.red;
        } else {
            text.color = Color.white;
        }
        text.text = $"@you: {rating}";
    }
}
