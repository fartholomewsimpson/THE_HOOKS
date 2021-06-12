using System;
using Relationships;
using UnityEngine;

namespace NeuralNetwork {
    public class NeuralNetwork : MonoBehaviour {
        public RelationshipEntity relationshipEntity;
        public Layer[] Layers { get; private set; }
        public event Action OnNodeUpdate;

        void Start() {
            if (relationshipEntity != null) {
                relationshipEntity.OnAddRelationship += AddRelationshipInputs;
            }

            // TODO: Temporary
            Layers = new Layer[1] { new Layer() };
        }

        void AddRelationshipInputs(RelationshipEntity relationship) {
            var layer = Layers[0];
            var nextLayer = Layers.Length > 1 ? Layers[1] : null;
            var ratingX = relationshipEntity.RelationshipRatings[relationship].x;
            var input = new Node { Value = ratingX };
            layer.Inputs.Add(input);
            if (nextLayer != null) {
                for (int i = 0; i < nextLayer.Inputs.Count; i++) {
                    var connection = new Connection {
                        Input = input,
                        Weight = UnityEngine.Random.value,
                    };
                    connection.Output = new Node {
                        Value = layer.Function(connection.Input.Value) * connection.Weight,
                    };
                    layer.Connections.Add(connection);
                }
            }

            if (OnNodeUpdate != null) {
                OnNodeUpdate();
            }
        }
    }
}
