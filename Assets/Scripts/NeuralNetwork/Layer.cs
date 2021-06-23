using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Neural {
    public class Layer {
        public List<Node> Nodes { get; set; }

        public Layer() {
            Nodes = new List<Node>();
        }

        public Layer(int count, int layerIndex) {
            Nodes = new List<Node>();
            for (int i = 0; i < count; i++) {
                Nodes.Add(new Node(Random.value, new Vector2(layerIndex, i)));
            }
        }

        public void AddNode(Node output) {
            foreach (var node in Nodes) {
                node.AddConnection(output, Random.value);
            }
        }

        public void RemoveNode(Node output) {
            foreach(var node in Nodes) {
                node.RemoveConnection(output);
            }
        }
    }
}
