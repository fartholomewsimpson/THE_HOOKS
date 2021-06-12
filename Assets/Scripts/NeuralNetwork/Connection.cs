using System;

namespace NeuralNetwork {
    public class Connection {
        public Node Input { get; set; }
        public Node Output { get; set; }
        public float Weight { get; set; }
    }
}
