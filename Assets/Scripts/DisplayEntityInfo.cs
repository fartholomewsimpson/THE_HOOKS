using Relationships;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEntityInfo : MonoBehaviour
{
    public string playerTag;
    public RelationshipEntity relationshipEntity;

    RelationshipEntity _playerEntity;
    Text _text;

    void Start() {
        _text = GetComponent<Text>();
        _playerEntity = GameObject.FindGameObjectWithTag(playerTag)?.GetComponent<RelationshipEntity>();
    }

    void Update() {
        if (_playerEntity == null)
            return;

        var rating = relationshipEntity.GetEntityRating(_playerEntity);
        if (rating > 0) {
            _text.color = Color.green;
        } else if (rating < 0) {
            _text.color = Color.red;
        } else {
            _text.color = Color.white;
        }
        _text.text = $"@you: {rating}";
    }
}
