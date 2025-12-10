using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    float spawnTimer;

    [SerializeField]
    float radius = 5f;

    [SerializeField]
    float minSpawnInterval = 3f;

    [SerializeField]
    float maxSpawnInterval = 7f;

    [SerializeField]
    public List<SpawnerItem> spawnerItems = new List<SpawnerItem>();

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
            SpawnEnemy();

        }
    }

    private void SpawnEnemy()
    {
        if (spawnerItems != null && spawnerItems.Count > 0)
        {
            Instantiate(SpawnerItem.ChooseItemFromList(spawnerItems), SelectSpawnPoint(), Quaternion.identity);
        }
    }

    private Vector3 SelectSpawnPoint()
    {
        for (int i = 0; i < 10; i++)
        {
            // random point in circle on XZ plane
            Vector2 r = Random.insideUnitCircle * radius;
            Vector3 candidate = transform.position + new Vector3(r.x, 0f, r.y);

            // sample navmesh near candidatewd
            NavMeshHit hit;
            if (NavMesh.SamplePosition(candidate, out hit, 2f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return transform.position;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}

