using UnityEngine;

namespace Relationships {
    public class RelationshipEvent {
        public RelationshipEntity Target { get; set; }
        public string Text { get; set; }
        public Vector2 Rating { get; set; }
    }
}
