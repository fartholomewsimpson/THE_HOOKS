using UnityEditor;
using UnityEngine;
using Neural;

namespace Editors {
    [CustomEditor(typeof(NeuralNetwork))]
    [CanEditMultipleObjects]
    public class NetworkEditor : Editor {
        public override void OnInspectorGUI() {
            base.DrawDefaultInspector();

            var network = (NeuralNetwork)target;

            GUILayout.Space(20);
            GUILayout.Label("Network Configuration");

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Open Editor")) {
                var window = EditorWindow.CreateWindow<NetworkWindow>();
                window.SetNeuralNetwork(network);
            }
            GUILayout.EndHorizontal();
        }
    }
}
