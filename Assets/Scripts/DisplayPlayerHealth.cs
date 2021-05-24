using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPlayerHealth : MonoBehaviour
{
    public GravityEntity entity;

    float _startingHealth;
    float _previousHealth;
    float _startingScale;

    void Start() {
        _startingHealth = entity.health;
        _previousHealth = _startingHealth;
        _startingScale = transform.localScale.y;
    }

    void Update() {
        if (entity.health > 0 && entity.health != _previousHealth) {
            var yScale = _startingScale * (entity.health)/_startingHealth;
            transform.localScale = new Vector3(
                transform.localScale.x,
                yScale,
                transform.localScale.z);
            _previousHealth = entity.health;
        }
    }
}
