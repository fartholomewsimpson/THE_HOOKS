using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Neural;
using System.Linq;

namespace Editors {
    public class NetworkWindow : EditorWindow {
        public Sprite circle;
        public float width = 100;
        public float spacingX = 160;
        public float spacingY = 120;
        public Color backgroundColor; // TODO: This doesn't appear to do anything where it's used
        public Color nodeColor;
        public int layerCount = 3;
        public int currentLayer = 0;
        public int curNodeCount = 3;
        public NeuralNetwork neuralNetwork;
        public float offsetX = 40f;
        public float offsetY = 140f;

        List<Layer> _layers;
        int _prevLayer;
        string _spritePath = "Assets/Textures/circle.png";

        [MenuItem("Window/Custom/Network")]
        public static void ShowWindow() {
            var networkWindow = EditorWindow.GetWindow(typeof(NetworkWindow)) as NetworkWindow;
        }

        public void SetNeuralNetwork(NeuralNetwork network) {
            neuralNetwork = network;
            if (neuralNetwork.layers == null) {
                RerollNetwork();
                Save();
            }
            
            _layers = new List<Layer>();
            foreach (var layer in neuralNetwork.layers) {
                _layers.Add(layer);
            }

            layerCount = _layers.Count;
            curNodeCount = _layers[currentLayer].Nodes.Count;
        }

        void OnEnable() {
            RerollNetwork();
            nodeColor = Color.white;
            circle = AssetDatabase.LoadAssetAtPath<Sprite>(_spritePath);
        }

        [System.Obsolete]
        void OnGUI() {
            RenderGUI();

            var frame = new Rect(offsetX, offsetY, 1400, 800);
            Handles.BeginGUI();
            Handles.DrawSolidRectangleWithOutline(frame, backgroundColor, Color.black);

            // draw nodes and connections
            foreach (var layer in _layers) {
                foreach (var node in layer.Nodes) {
                    var nodePosition = GetNodePosition(node.Position);
                    BeginWindows();
                    foreach (var connection in node.Connections) {
                        var endPosition = GetNodePosition(connection.Output.Position);
                        Handles.BeginGUI();
                        Handles.color = Color.white;
                        Handles.DrawLine(nodePosition, endPosition, 20);
                        Handles.EndGUI();
                    }

                    var intensity = Mathf.Max(.3f, node.Value);
                    var rect = new Rect(nodePosition.x - width/2, nodePosition.y - width/2, width, width);
                    GUI.color = new Color(nodeColor.r * intensity, nodeColor.g * intensity, nodeColor.b * intensity);
                    GUI.DrawTexture(rect, circle.texture);

                    var floatVal = EditorGUI.FloatField(
                        new Rect(rect.x+(rect.width/4), rect.y+(rect.height/4), width/2, width/2),
                        node.Value,
                        new GUIStyle {
                            alignment = TextAnchor.MiddleCenter,
                            border = new RectOffset(),
                        }
                    );
                    node.Value = Mathf.Min(1, Mathf.Max(0, floatVal));
                    EndWindows();
                }
            }

            Handles.EndGUI();
        }

        Vector2 GetNodePosition(Vector2 nodeIndex)
            => new Vector2(offsetX + (spacingX*nodeIndex.x) + width/2, offsetY + (spacingY*nodeIndex.y) + width/2);

        // TODO: UPDATE OBSOLETE!!!
        [System.Obsolete]
        void RenderGUI () {
            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical();
            circle = EditorGUILayout.ObjectField(circle, typeof(Sprite)) as Sprite;
            width = EditorGUILayout.FloatField("Width", width);
            spacingX = EditorGUILayout.FloatField("X Spacing", spacingX);
            spacingY = EditorGUILayout.FloatField("Y Spacing", spacingY);
            backgroundColor = EditorGUILayout.ColorField("Background Color", backgroundColor);
            nodeColor = EditorGUILayout.ColorField("Node Color", nodeColor);
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            if (GUILayout.Button("Reroll")) {
                RerollNetwork();
            }
            layerCount = EditorGUILayout.IntSlider("Layer Count", layerCount, 1, 5);
            if (_layers.Count > layerCount) { // remove layers
                for (int i = _layers.Count-1; i >= layerCount; i--) {
                    var deadLayer = _layers[i];
                    if (i > 0) {
                        foreach (var node in deadLayer.Nodes) {
                            _layers[i-1].RemoveNode(node);
                        }
                    }
                    _layers.Remove(deadLayer);
                }
            } else if (_layers.Count < layerCount) { // add layers
                for (int i = _layers.Count; i < layerCount; i++) {
                    _layers.Add(new Layer(curNodeCount, i));
                    foreach (var node in _layers[i].Nodes) {
                        _layers[i-1].AddNode(node);
                    }
                }
            }

            _prevLayer = currentLayer;
            currentLayer = EditorGUILayout.IntSlider("Current Layer", currentLayer, 0, layerCount-1);
            if (currentLayer != _prevLayer) {
                curNodeCount = _layers[currentLayer].Nodes.Count;
                _prevLayer = currentLayer;
            }
            curNodeCount = EditorGUILayout.IntSlider("Node Count", curNodeCount, 1, 7);
            var prevNodeCount = _layers[currentLayer].Nodes.Count;
            if (prevNodeCount > curNodeCount) { // remove nodes
                for (int i = prevNodeCount-1; i >= curNodeCount; i--) {
                    var deadNode = _layers[currentLayer].Nodes[i];
                    if (currentLayer > 0) {
                        _layers[currentLayer-1].RemoveNode(deadNode);
                    }
                    _layers[currentLayer].Nodes.Remove(deadNode);
                }
            } else if (prevNodeCount < curNodeCount) { // add nodes
                for (int i = prevNodeCount; i < curNodeCount; i++) {
                    var node = new Node(Random.value, new Vector2(currentLayer, i));
                    _layers[currentLayer].Nodes.Add(node);
                    if (currentLayer+1 < _layers.Count) {
                        node.AddLayer(_layers[currentLayer+1]);
                    }
                    if (currentLayer > 0) {
                        _layers[currentLayer-1].AddNode(node);
                    }
                }
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            neuralNetwork = EditorGUILayout.ObjectField(neuralNetwork, typeof(NeuralNetwork)) as NeuralNetwork;
            if (GUILayout.Button("Save") && neuralNetwork != null) {
                Save();
            }
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
        }

        void RerollNetwork() {
            if (_layers == null) {
                _layers = new List<Layer>();
            }
            _layers.Clear();
            for (int x = 0; x < layerCount; x++) {
                _layers.Add(new Layer(curNodeCount, x));
            }

            int i = 0;
            foreach (var layer in _layers) {
                if (i != _layers.Count-1) {
                    foreach (var node in layer.Nodes) {
                        foreach (var nextNode in _layers[i+1].Nodes) {
                            node.AddConnection(nextNode, Random.value);
                        }
                    }
                }
                i++;
            }
        }

        void Save() {
            neuralNetwork.layers = _layers;
            Debug.Log("SAVED");
        }
    }
}
