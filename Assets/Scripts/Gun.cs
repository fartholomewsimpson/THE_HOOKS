using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour {
    public GameObject blastParticles; 
    public GameObject shell;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public LayerMask layerMask;
    public float bulletDistance = 10;
    public float strength = 50;
    public Player player;

    bool _flipped;

    void Update() {
        // TODO: Maybe for this, flipped should be stored somewhere aside from the sprite renderer
        // to remove the wierd dependency.
        if (spriteRenderer.flipX != _flipped) {
            _flipped = spriteRenderer.flipX;
            transform.localPosition = new Vector2(transform.localPosition.x * -1, transform.localPosition.y);
        }
    }

    public void Shoot(InputAction.CallbackContext context) {
        if (context.ReadValueAsButton()) {
            if (!player.aiming)
                animator.SetTrigger("Shootin");
            
            var direction = spriteRenderer.flipX ? Vector2.left : Vector2.right;
            if (player.aiming) {
                direction.y = .5f;
                if (player.downward) {
                    direction.y *= -1;
                }
                direction = direction.normalized;
            }

            var hit = Physics2D.Raycast(transform.position, direction, bulletDistance, layerMask);
            if (hit.collider != null && hit.collider.TryGetComponent<GravityEntity>(out GravityEntity entity)) {
                entity.TakeDamage(strength, direction);
            }
            if (context.started) {
                GameObject.Instantiate(
                    shell,
                    new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    Quaternion.identity,
                    transform);
                GameObject.Instantiate(blastParticles, hit.point, Quaternion.identity);
            }
        } else {
            animator.ResetTrigger("Shootin");
        }
    }
}
