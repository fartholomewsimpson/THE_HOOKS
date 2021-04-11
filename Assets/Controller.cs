using StateStuff;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Transform leftFoot, rightFood;
    public LayerMask collidables;

    Rigidbody2D rb;
    StateMachine stateMachine;
    float footRadius = .1f;
    bool isGrounded = false;
    float jumpSpeed = 10f;
    float gravity = 1;
    float xVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();
    }

    void Update()
    {
        // isGrounded =
        //     Physics2D.OverlapCircle(leftFoot.position, footRadius, collidables) ||
        //     Physics2D.OverlapCircle(rightFood.position, footRadius, collidables);

        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        // TODO: add this
        // xVelocity = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        // rb.AddForce(new Vector2(xVelocity, 0));

        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        else
        {
            var fallSpeed = -gravity + rb.velocity.y;
            rb.velocity = new Vector2(rb.velocity.x, fallSpeed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // if (other.gameObject.layer == collidables)
        isGrounded = true;
        var contact = collision.GetContact(0);
        transform.position = new Vector2(
            transform.position.x,
            transform.position.y - contact.separation);
    }
}
