using UnityEngine;
using UnityEngine.AI;

public class TriggerEnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject _level;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int spawnAmount = 1;

    private bool alreadySpawned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !alreadySpawned)
        {
            alreadySpawned = true;
            Spawn();
        }
    }

    private void Spawn()
    {
        for(int i = 0; i < spawnAmount; i++)
        {
            _spawnPoint.transform.position = new Vector3(_spawnPoint.transform.position.x - i, _spawnPoint.transform.position.y, _spawnPoint.transform.position.z);
            GameObject spawnedObject = Instantiate(_enemyPrefab, _spawnPoint.position, _spawnPoint.rotation);
            spawnedObject.transform.SetParent(_level.transform);
            spawnedObject.GetComponent<NavMeshAgent>().enabled = true;
            GameObject target = GameObject.Find("Boatman Shadow");
            spawnedObject.GetComponent<EnemyAI>()._Traget = target.transform;
        }
    }
}
