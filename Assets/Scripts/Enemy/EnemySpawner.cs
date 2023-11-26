using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _spawnersEnemy;
    [SerializeField] private List<GameObject> _enemyTypes;
    [SerializeField] private List<GameObject> _enemySpawned;
    [SerializeField] private int _minEnemyCount;
    [SerializeField] private int _maxEnemyCount;
    [SerializeField] private int _countEnemys;
    [SerializeField] private int _countKilledEnemys;

    private void Awake()
    {
        _spawnersEnemy.AddRange(GameObject.FindGameObjectsWithTag("SpawnerEnemy"));

        _countEnemys = Random.Range(_minEnemyCount, _maxEnemyCount);

        StartCoroutine(DelaySpawner());
    }

    private void Spawn()
    {
        GameObject randomPlace = _spawnersEnemy[Random.Range(0, _spawnersEnemy.Count - 1)];
        GameObject randomEnemyType = _enemyTypes[Random.Range(0, _enemyTypes.Count - 1)];

        _enemySpawned.Add(Instantiate(randomEnemyType, randomPlace.transform.position, randomPlace.transform.rotation));
    }

    private void CheckDiedEnemys()
    {
        if (_countEnemys == _countKilledEnemys)
        {
            PlayerActions.GameWin();
        }
    }

    private void PlusKilledEnemy()
    {
        _countKilledEnemys++;
        CheckDiedEnemys();
    }
    
    private IEnumerator DelaySpawner()
    {
        yield return new WaitForSeconds(2);

        Spawn();

        if (_enemySpawned.Count < _countEnemys) 
        {
            StartCoroutine(DelaySpawner());
        }
    }

    private void OnEnable()
    {
        EnemyActions.EnemyKilled += PlusKilledEnemy;
    }

    private void OnDisable()
    {
        EnemyActions.EnemyKilled -= PlusKilledEnemy;
    }
}