using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Neural {
    public class DisplayNetwork : MonoBehaviour {
        public NeuralNetwork network;
        public GameObject nodePrefab, connectionPrefab;
        public Text title;
        public float spacing = 20;

        Dictionary<Node, NodeData> _nodes;
        Vector2 _bottomLeft;
        RectTransform _rectTransform;

        class NodeData {
            public Transform transform;
            public Text text;
        }

        void Start() {
            _rectTransform = GetComponent<RectTransform>();

            _nodes = new Dictionary<Node, NodeData>();
            _bottomLeft = new Vector2(_rectTransform.rect.xMin, _rectTransform.rect.yMin);
        }

        // TODO: What to do about this?
        // void Refresh() {
        //     title.text = network.gameObject.name;

        //     if (_nodes.Count > 0)
        //         DeleteNodes();

        //     for (int i = 0; i < network.Layers.Length; i++) {
        //         // Inputs
        //         for (int j = 0; j < network.Layers[i].Inputs.Count; j++) {
        //             AddNode(
        //                 new Vector2((i * spacing) + (spacing/2), (j * spacing) + (spacing/2)),
        //                 network.Layers[i].Inputs[j]
        //             );
        //         }

        //         // Connections
        //         foreach (var con in network.Layers[i].Connections) {
        //             var line = Instantiate(connectionPrefab, transform.position, Quaternion.identity, transform)
        //                 .GetComponent<LineRenderer>();
        //             // line.SetPositions(new Vector3[] {
        //             //     _nodes[con.Input].transform.localPosition,
        //             //     _nodes[con.Output].transform.localPosition,
        //             // });
        //         }
        //     }
        // }

        void AddNode(Vector2 position, Node node) {
            var obj = GameObject.Instantiate(nodePrefab, transform.position, Quaternion.identity, transform);
            obj.transform.localPosition = _bottomLeft + position;
            var nodeData = new NodeData {
                transform = obj.transform,
                text = obj.GetComponentInChildren<Text>(),
            };
            nodeData.text.text = node.Value.ToString();

            _nodes.Add(node, nodeData);
        }

        void DeleteNodes() {
            foreach (var node in _nodes) {
                GameObject.DestroyImmediate(node.Value.transform.gameObject);
            }
            _nodes.Clear();
        }
    }
}
