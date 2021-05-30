using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GravityEntity))]
public class ZombieSpawner : MonoBehaviour
{
    public GameObject explosion;
    public GameObject zombiePrefab;
    public int childCount = 10;
    public float spawnRate = 3;

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
            StartCoroutine(SpawnZombie());
        }
    }

    public IEnumerator SpawnZombie() {
        if (!_spawning) {
            _spawning = true;
            yield return new WaitForSeconds(spawnRate);

            var location = new Vector2(transform.position.x, transform.position.y + 1);
            var zombie = GameObject.Instantiate(zombiePrefab, location, Quaternion.identity);
            _children.Add(zombie);
            _spawning = false;
        }
    }
}
