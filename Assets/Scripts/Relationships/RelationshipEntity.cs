using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Relationships {
    public class RelationshipEntity : MonoBehaviour {
        public Collider2D lineOfSight;
        public LayerMask layerMask;
        public int maxVisible = 10;

        public float GetEntityRating(RelationshipEntity entity) {
            return _relationshipRatings
                .Where(kvp => kvp.Key == entity)
                .Sum(kvp => kvp.Value);
        }

        [SerializeField] List<RelationshipEntity> _relationships;
        [SerializeField] Dictionary<RelationshipEntity, float> _relationshipRatings;
        [SerializeField] List<RelationshipEvent> _events;
        [SerializeField] Dictionary<RelationshipEvent, float> _eventRatings;

        [SerializeField] float _currentSituationRating;

        void Start() {
            _relationships = new List<RelationshipEntity>();
            _relationshipRatings = new Dictionary<RelationshipEntity, float>();
            _events = new List<RelationshipEvent>();
            _eventRatings = new Dictionary<RelationshipEvent, float>();
        }

        void Update() {
            var contactFilter = new ContactFilter2D { useLayerMask = true, layerMask = layerMask };
            var visibleColliders = new Collider2D[maxVisible];
            if (lineOfSight != null && Physics2D.OverlapCollider(lineOfSight, contactFilter, visibleColliders) > 0) {
                for (int i = 0; i < visibleColliders.Length; i++) {
                    var col = visibleColliders[i];
                    var relationship = col?.GetComponent<RelationshipEntity>();
                    if (relationship?.gameObject != this.gameObject && !_relationships.Contains(relationship)) {
                        _relationships.Add(relationship);
                    }
                }
            }
        }
    }
}
