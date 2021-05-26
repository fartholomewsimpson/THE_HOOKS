using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerHealthText : MonoBehaviour
{
    public GravityEntity gravityEntity;
    Text _text;
    
    float _startHealth;

    void Start() {
        _text = GetComponent<Text>();

        _startHealth = gravityEntity.health;
    }

    void Update() {
        _text.text = $"{gravityEntity.health}/{_startHealth}";
    }
}
