using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class Lasso : MonoBehaviour
{
    public GravityEntity entity;
    public Animator animator;
    public SpriteRenderer parentSprite;
    public LayerMask layerMask;

    LineRenderer _lassoRenderer;
    Camera _camera;

    [SerializeField] bool _flipped;
    [SerializeField] bool _lassoing;

    void Start() {
        _camera = Camera.main;
        _lassoRenderer = GetComponent<LineRenderer>();
    }

    void Update() {
        // TODO: Make this more better
        if (parentSprite.flipX != _flipped) {
            _flipped = parentSprite.flipX;
            transform.localPosition = new Vector2(transform.localPosition.x * -1, transform.localPosition.y);
        }
    }

    public void ThrowLasso(InputAction.CallbackContext context) {
        if (context.ReadValueAsButton()) {
            if (!_lassoing) {
                _lassoing = true;

                var mousePosition = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

                _lassoRenderer.enabled = true;

                var hit = Physics2D.Raycast(transform.position, mousePosition - transform.position, 1000, layerMask);

                Debug.DrawRay(transform.position, hit.point, Color.red, 1);
                Debug.Log(hit.distance);

                _lassoRenderer.startWidth = .1f;
                _lassoRenderer.endWidth = .1f;
                _lassoRenderer.SetPositions(new List<Vector3>{
                    transform.position,
                    hit.point,
                }.ToArray());

                StartCoroutine(Zip(hit.point));
            }
        } else {
            _lassoRenderer.enabled = false;
            _lassoing = false;
        }
    }

    IEnumerator Zip(Vector2 hitPoint) {
        yield return new WaitForSeconds(.5f);
        transform.parent.position = hitPoint;
    }
}
