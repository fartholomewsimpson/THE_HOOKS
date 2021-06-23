using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Neural {
    public class Node {
        public float Value { get; set; }
        public Vector2 Position { get; set; } // TODO: move this eventually maybe
        public List<Connection> Connections { get; private set; }

        public Node(float value, Vector2 position) {
            Value = value;
            Position = position;
            Connections = new List<Connection>();
        }

        public void AddConnection(Node node, float? weight = null) {
            if (!weight.HasValue) {
                weight = Random.value;
            }
            var connection = new Connection {
                Input = this,
                Output = node,
                Weight = weight.Value,
            };
            Connections.Add(connection);
        }

        public void RemoveConnection(Node output) {
            var target = Connections.Where(c => c.Output == output).FirstOrDefault();
            Connections.Remove(target);
        }

        public void AddLayer(Layer layer) {
            foreach (var node in layer.Nodes) {
                Connections.Add(new Connection {
                    Input = this,
                    Output = node,
                    Weight = Random.value,
                });
            }
        }
    }
}
