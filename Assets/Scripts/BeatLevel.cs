using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeatLevel : MonoBehaviour
{
    public string nextScene;
    public LayerMask layerMask;

    BoxCollider2D _collider;
    Canvas _canvas;


    void Start() {
        _collider = GetComponent<BoxCollider2D>();
        _canvas = GetComponentInChildren<Canvas>();
    }

    void FixedUpdate() {
        var overlaps = Physics2D.OverlapBox(_collider.bounds.center, _collider.bounds.extents, 0, layerMask);
        if (overlaps != null) {
            _canvas.enabled = true;
            StartCoroutine(NextLevel());
        }
    }

    IEnumerator NextLevel () {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(nextScene);
    }
}
