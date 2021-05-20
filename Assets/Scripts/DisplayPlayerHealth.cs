using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPlayerHealth : MonoBehaviour
{
    public Player player;

    float _startingHealth;
    float _previousHealth;
    float _startingScale;

    void Start() {
        _startingHealth = player.health;
        _previousHealth = _startingHealth;
        _startingScale = transform.localScale.y;
    }

    void Update() {
        if (player.health != _previousHealth) {
            var yScale = _startingScale * (player.health)/_startingHealth;
            transform.localScale = new Vector3(
                transform.localScale.x,
                yScale,
                transform.localScale.z);
            _previousHealth = player.health;
        }
    }
}
