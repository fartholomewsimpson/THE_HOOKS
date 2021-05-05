using UnityEngine;
using Utils;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class GravityEntityAnimator : MonoBehaviour
{
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    GravityEntity _gravityEntity;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _gravityEntity = this.gameObject.GetComponentInHeirarchy<GravityEntity>();
    }

    void FixedUpdate()
    {
        _animator.SetFloat("XSpeed", Mathf.Abs(_gravityEntity.velocity.x));
        _animator.SetFloat("YVelocity", _gravityEntity.velocity.y);

        if (_gravityEntity.velocity.x > .001f) {
            _spriteRenderer.flipX = false;
        }
        if (_gravityEntity.velocity.x < -.001f) {
            _spriteRenderer.flipX = true;
        }
    }
}
