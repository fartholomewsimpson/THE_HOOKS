using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GravityEntity))]
[RequireComponent(typeof(SpriteRenderer))]
public class Fly : MonoBehaviour
{
    public float range = 10;
    public float speed;
    public float attentionTimeMax;
    public float attentionTimeMin;

    [SerializeField] bool _switchPoints = true;
    [SerializeField] Vector2 _direction;
    GravityEntity _gravityEntity;
    SpriteRenderer _spriteRenderer;

    void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gravityEntity = GetComponent<GravityEntity>();
        _gravityEntity.AfterGravity += DoStuff;
    }

    void DoStuff() {
        if (_gravityEntity.velocity.x > 0) {
            _spriteRenderer.flipX = false;
        } else {
            _spriteRenderer.flipX = true;
        }
        StartCoroutine(RandomPoints());
    }

    IEnumerator RandomPoints() {
        if (_switchPoints) {
            _switchPoints = false;
            _direction = new Vector2(Random.Range(-range, range), Random.Range(-range, range)).normalized;
            _gravityEntity.velocity = _direction * speed;
            yield return new WaitForSeconds(Random.Range(attentionTimeMin, attentionTimeMax));
            _switchPoints = true;
        }
    }
}
