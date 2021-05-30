using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Relationships {
    public class RelationshipEntity : MonoBehaviour {
        public Collider2D lineOfSight;
        public LayerMask layerMask;
        public int maxVisible = 10;

        public List<RelationshipEntity> Relationships { get; private set; } = new List<RelationshipEntity>();
        public Dictionary<RelationshipEntity, Vector2> RelationshipRatings { get; private set; } = new Dictionary<RelationshipEntity, Vector2>();
        public List<RelationshipEvent> Events { get; private set; } = new List<RelationshipEvent>();
        public Dictionary<RelationshipEvent, Vector2> EventRatings { get; private set; } = new Dictionary<RelationshipEvent, Vector2>();
        public Vector2 CurrentSituationRating { get; private set; }

        public float GetEntityRating(RelationshipEntity entity) {
            // TODO: Make this rating system combine the two values in some intelligent way,
            //        or just get rid of it entirely or something.
            return RelationshipRatings
                .Where(kvp => kvp.Key == entity)
                .Sum(kvp => kvp.Value.x);
        }

        public void AddRelationship(RelationshipEntity entity) {
            Relationships.Add(entity);
            AddEvent();
        }

        public void AddEvent() {
            // TODO: Do this
        }

        void Update() {
            var contactFilter = new ContactFilter2D { useLayerMask = true, layerMask = layerMask };
            var visibleColliders = new Collider2D[maxVisible];
            if (lineOfSight != null && Physics2D.OverlapCollider(lineOfSight, contactFilter, visibleColliders) > 0) {
                for (int i = 0; i < visibleColliders.Length; i++) {
                    var col = visibleColliders[i];
                    var relationship = col?.GetComponent<RelationshipEntity>();
                    if (relationship?.gameObject != this.gameObject && !Relationships.Contains(relationship)) {
                        AddRelationship(relationship);
                    }
                }
            }
        }
    }
}
