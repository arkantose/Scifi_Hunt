using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Singleton
    private static SpawnManager _instance;
    public static SpawnManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("The SpawnManager is Null");

            return _instance;
        }
    }

    // Enemy Container Lists for Enemy Pool
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField]
    private List<GameObject> _enemyPool = new List<GameObject>();
    //Game is not over
    private bool _gameOver = false;
    //Where to Spawn Enemies
    [SerializeField]
    private GameObject _spawnEnemy;
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _enemyPool = GenerateEnemies(15);
        StartCoroutine(StartEnemySpawn());
    }

    private void Update()
    {
        
    }

    List<GameObject> GenerateEnemies(int amountOfEnemies)
    {
        for (int i = 0; i < amountOfEnemies; i++)
        {
            GameObject enemy = Instantiate(_enemies[Random.Range(0, 2)]);
            enemy.transform.parent = _enemyContainer.transform;
            enemy.SetActive(false);
            _enemyPool.Add(enemy);
        }

        return _enemyPool;
    }

    IEnumerator StartEnemySpawn()
    {
        while (_gameOver == false)
        {
            yield return new WaitForSeconds(Random.Range(.5f, 2f));
            GameObject enemy = RequestEnemy();
            enemy.transform.position = _spawnEnemy.transform.position;
        }
    }

    public GameObject RequestEnemy()
    {
        foreach (var enemy in _enemyPool)
        {
            if (enemy.activeInHierarchy == false)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }
        GameObject newEnemy = Instantiate(_enemies[Random.Range(0, 2)]);
        newEnemy.transform.parent = _enemyContainer.transform;
        newEnemy.SetActive(false);
        _enemyPool.Add(newEnemy);
        return newEnemy;

        
    }
}
