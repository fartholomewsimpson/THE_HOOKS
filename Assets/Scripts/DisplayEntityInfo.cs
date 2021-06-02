using Relationships;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEntityInfo : MonoBehaviour
{
    public string playerTag;
    public RelationshipEntity relationshipEntity;
    public Text text;

    RelationshipEntity _playerEntity;

    void Start() {
        var player = GameObject.FindGameObjectWithTag(playerTag);
        if (player == null)
            return;
        _playerEntity = player.GetComponent<RelationshipEntity>();
    }

    void Update() {
        text.text = "";

        if (_playerEntity != null) {
            var rating = relationshipEntity.GetEntityRating(_playerEntity);
            if (rating > 0) {
                text.color = Color.green;
            } else if (rating < 0) {
                text.color = Color.red;
            } else {
                text.color = Color.white;
            }
            text.text += $"@you: {rating}\n";
        }

        if (relationshipEntity != null) { 
            text.text += $"relationships: {relationshipEntity.Relationships.Count}\n";
        }
    }
}
