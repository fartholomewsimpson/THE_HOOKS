using Relationships;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayEntityInfo : MonoBehaviour
{
    public string statusTag;
    public string playerTag;
    public RelationshipEntity relationshipEntity;
    [SerializeField] bool _displayStatuses;

    RelationshipEntity _playerEntity;
    Text _text;

    public void ToggleStatuses() {
        SetStatuses(!_displayStatuses);
    }

    public bool GetStatus() {
        return _displayStatuses;
    }

    public void SetStatuses(bool status) {
        _displayStatuses = status;
        var statuses = GameObject.FindGameObjectsWithTag(statusTag);
        foreach (var s in statuses) {
            var canvas = s.GetComponent<Canvas>();
            if (canvas != null) {
                canvas.enabled = status;
            } 
        }
    }

    void Start() {
        _text = GetComponent<Text>();
        var player = GameObject.FindGameObjectWithTag(playerTag);
        if (player == null)
            return;
        _playerEntity = player.GetComponent<RelationshipEntity>();
    }

    void Update() {
        _text.text = "";

        if (_playerEntity != null) {
            var rating = relationshipEntity.GetEntityRating(_playerEntity);
            if (rating > 0) {
                _text.color = Color.green;
            } else if (rating < 0) {
                _text.color = Color.red;
            } else {
                _text.color = Color.white;
            }
            _text.text += $"@you: {rating}\n";
        }

        if (relationshipEntity != null) { 
            _text.text += $"relationships: {relationshipEntity.Relationships.Count}\n";
        }
    }
}
