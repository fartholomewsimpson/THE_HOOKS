using System;
using System.Collections.Generic;
using System.Linq;
using Relationships;
using UnityEngine;

namespace NeuralNetwork {
    public class Node {
        public string Name { get; set; }
        public float Value { get; set; }
        public List<Connection> Connections { get; set; }
    }

    public class Connection {
        public Connection (Action action) {
            Weight = UnityEngine.Random.value;
            Action = action;
        }

        public float Weight { get; set; }
        public Action Action { get; set; }
    }

    public class NeuralNetwork : MonoBehaviour {
        public RelationshipEntity entity;

        List<Node> _inputs;
        Dictionary<string, Action> _actions;

        void Start() {
            _actions = new Dictionary<string, Action> {
                // TODO: How should these be defined
                { "Wait", () => Debug.Log($"{this.gameObject.name}: WAIT") },
                { "TurnAround", () => Debug.Log($"{this.gameObject.name}: TURN AROUND") },
            };

            _inputs = new List<Node>();
            foreach (var relationship in entity.Relationships) {
                var rating = relationship.RelationshipRatings[relationship];
                var relPosition = relationship.transform.position - transform.position;
                var relDistance = relPosition.magnitude;

                _inputs.AddRange(new List<Node> {
                    new Node {
                        Name = "rating.x",
                        Value = rating.x,
                        Connections = _actions.Select(a => new Connection(a.Value)).ToList(),
                    },
                    new Node {
                        Name = "rating.y",
                        Value = rating.y,
                        Connections = _actions.Select(a => new Connection(a.Value)).ToList(),
                    },
                    new Node {
                        Name = "relPosition.x",
                        Value = relPosition.x,
                        Connections = _actions.Select(a => new Connection(a.Value)).ToList(),
                    },
                    new Node {
                        Name = "relPosition.y",
                        Value = relPosition.y,
                        Connections = _actions.Select(a => new Connection(a.Value)).ToList(),
                    },
                    new Node {
                        Name = "relDistance",
                        Value = relDistance,
                        Connections = _actions.Select(a => new Connection(a.Value)).ToList(),
                    },
                });
            }
        }

        void Update() {
            // basically, decide what to do and do it
        }
    }
}
