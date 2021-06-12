using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Relationships {
    public class RelationshipEntity : MonoBehaviour {
        public new string name;
        public Collider2D lineOfSight;
        public LayerMask layerMask;
        public int maxVisible = 10;

        public List<RelationshipEntity> Relationships { get; private set; } = new List<RelationshipEntity>();
        public Dictionary<RelationshipEntity, Vector2> RelationshipRatings { get; private set; } = new Dictionary<RelationshipEntity, Vector2>();
        public List<RelationshipEvent> Events { get; private set; } = new List<RelationshipEvent>();
        public string Name { get; private set; }

        public float GetEntityRating(RelationshipEntity entity) {
            // TODO: Make this rating system combine the two values in some intelligent way,
            //        or just get rid of it entirely or something.
            return RelationshipRatings
                .Where(kvp => kvp.Key == entity)
                .Sum(kvp => kvp.Value.x);
        }

        public event Action<RelationshipEntity> OnAddRelationship;

        void Start() {
            Name = $"{name}_{UnityEngine.Random.Range(0, 1000)}";
        }

        // TODO: Finding visible entities should be done somewhere else, like gravity entity or something?
        void Update() {
            // Right now, just looks at currently visible entities and adds new ones
            var contactFilter = new ContactFilter2D { useLayerMask = true, layerMask = layerMask };
            var visibleColliders = new Collider2D[maxVisible];
            if (lineOfSight != null && Physics2D.OverlapCollider(lineOfSight, contactFilter, visibleColliders) > 0) {
                for (int i = 0; i < visibleColliders.Length; i++) {
                    var col = visibleColliders[i];
                    if (col != null) {
                        var relationship = col?.GetComponent<RelationshipEntity>();
                        if (relationship != null &&
                            relationship?.gameObject != this.gameObject &&
                            !Relationships.Contains(relationship)
                        ) {
                            AddRelationship(relationship);
                        }
                    }
                }
            }
        }

        void AddRelationship(RelationshipEntity relationship) {
            Relationships.Add(relationship);
            var relationshipEvent = new RelationshipEvent {
                Target = relationship,
                Rating = Vector2.zero,
                Text = $"Met {relationship.Name}",
            };
            Events.Add(relationshipEvent);
            RelationshipRatings[relationship] = relationshipEvent.Rating;

            if (OnAddRelationship != null) {
                OnAddRelationship(relationship);
            }
        }
    }
}
