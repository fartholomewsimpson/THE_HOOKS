using System;
using System.Collections.Generic;

namespace NeuralNetwork {
    public class Layer {
        public List<Node> Inputs { get; set; }
        public List<Connection> Connections { get; set; }
        public Func<float, float> Function { get; set; }

        public Layer() {
            Inputs = new List<Node>();
            Connections = new List<Connection>();
        }
    }
}
