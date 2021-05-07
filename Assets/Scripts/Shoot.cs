using UnityEngine;

public class Shoot : MonoBehaviour {
    public Transform shootOrigin;
    public GameObject blastParticles; 

    Animator _animator;
    SpriteRenderer _spriteRenderer;

    [SerializeField] bool _flipped;

    void Start() {
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update() {
        // TODO: Make this more better
        if (_spriteRenderer.flipX != _flipped) {
            _flipped = _spriteRenderer.flipX;
            shootOrigin.localPosition = new Vector2(shootOrigin.localPosition.x * -1, shootOrigin.localPosition.y);
        }

        if (Input.GetMouseButtonDown(0)) {
            _animator.SetTrigger("Shootin");
            Shootin();
        } else if (Input.GetMouseButtonUp(0)) {
            _animator.ResetTrigger("Shootin");
        }
    }

    void Shootin() {
        var hit = Physics2D.Raycast(shootOrigin.position, _spriteRenderer.flipX ? Vector2.left : Vector2.right);
        if (hit.collider.TryGetComponent<GravityEntity>(out GravityEntity entity)) {
            entity.GetHit();
        }
        GameObject.Instantiate(blastParticles, hit.point, Quaternion.identity);
    }
}
