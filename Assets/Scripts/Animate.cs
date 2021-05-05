using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Animate : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    public EntityData playerData;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        animator.SetFloat("XSpeed", Mathf.Abs(playerData.velocity.x));
        animator.SetFloat("YVelocity", playerData.velocity.y);

        if (playerData.velocity.x > .001f) {
            spriteRenderer.flipX = false;
        }
        if (playerData.velocity.x < -.001f) {
            spriteRenderer.flipX = true;
        }
    }
}
