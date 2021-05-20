using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GravityEntity))]
public class BulletShell : MonoBehaviour
{
    public Vector2 direction;
    public float rotationSpeed = 10;

    float _angle;
    GravityEntity _gravityEntity;

    void Start() {
        _gravityEntity = GetComponent<GravityEntity>();

        direction = new Vector2(-Random.value/5f, Random.value/5f);
        _gravityEntity.velocity = direction;

        StartCoroutine(KillYoself());
    }
    void Update() {
        // _angle = _angle + rotationSpeed;
        // transform.Rotate(Vector3.forward, _angle);
    }

    IEnumerator KillYoself() {
        yield return new WaitForSeconds(2);

        GameObject.DestroyImmediate(this.gameObject);
    }
}
