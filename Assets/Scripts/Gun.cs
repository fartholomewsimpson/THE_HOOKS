using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour {
    public GameObject blastParticles; 
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    [SerializeField] bool _flipped;

    void Update() {
        // TODO: Make this more better
        if (spriteRenderer.flipX != _flipped) {
            _flipped = spriteRenderer.flipX;
            transform.localPosition = new Vector2(transform.localPosition.x * -1, transform.localPosition.y);
        }
    }

    public void Shoot(InputAction.CallbackContext context) {
        if (context.ReadValueAsButton()) {
            animator.SetTrigger("Shootin");
            var hit = Physics2D.Raycast(transform.position, spriteRenderer.flipX ? Vector2.left : Vector2.right);
            if (hit.collider.TryGetComponent<GravityEntity>(out GravityEntity entity)) {
                entity.TakeDamage(10);
            }
            GameObject.Instantiate(blastParticles, hit.point, Quaternion.identity);
        } else {
            animator.ResetTrigger("Shootin");
        }
    }
}
