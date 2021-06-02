using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GravityEntity))]
public class Spawner : MonoBehaviour
{
    public new Camera camera;
    public GameObject explosion;
    public GameObject entityPrefab;
    public int childCount = 10;
    public float spawnRate = 3;
    public float width = 3;

    [SerializeField] bool _spawning;
    [SerializeField] List<GameObject> _children;
    GravityEntity _gravityEntity;

    void Start() {
        _children = new List<GameObject>(childCount);
        _gravityEntity = GetComponent<GravityEntity>();
        _gravityEntity.Hit += Die;
    }

    void Die(float amount, Vector2 direction) {
        GameObject.Instantiate(explosion);
        GameObject.Destroy(this);
    }

    void Update() {
        if (_children.Count < childCount) {
            StartCoroutine(SpawnEntity());
        }
    }

    public IEnumerator SpawnEntity() {
        if (!_spawning) {
            _spawning = true;
            yield return new WaitForSeconds(spawnRate);

            var randX = Random.Range(-width, width);
            var randY = Random.Range(0, width);
            var location = new Vector2(transform.position.x + randX, transform.position.y + 1 + randY);
            var entity = GameObject.Instantiate(entityPrefab, location, Quaternion.identity);
            var canvas = entity.GetComponent<Canvas>();
            if (canvas != null) {
                canvas.worldCamera = camera;
            }
            _children.Add(entity);
            _spawning = false;
        }
    }
}
