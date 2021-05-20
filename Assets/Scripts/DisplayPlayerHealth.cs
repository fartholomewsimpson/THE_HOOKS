using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPlayerHealth : MonoBehaviour
{
    public Player player;

    float _startingHealth;
    float _previousHealth;

    void Start() {
        _startingHealth = player.health;
        _previousHealth = _startingHealth;
    }

    void Update() {
        if (player.health != _previousHealth) {
            var yScale = transform.localScale.y * (player.health)/_startingHealth;
            transform.localScale = new Vector3(
                transform.localScale.x,
                yScale,
                transform.localScale.z);
            _previousHealth = player.health;
        }
    }
}
