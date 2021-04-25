using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [Header("Boundaries")] 
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;
    
    [SerializeField] private float timeForThrow;
    [SerializeField] private List<GameObject> enemiesToSpawn;
    [SerializeField] private Transform spawnPoint;
    
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private List<GameObject> activeEnemies = new List<GameObject>();

    private static EnemyManager sharedInstace;

    private void Awake()
    {
        sharedInstace = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        Initialize();
        
        //Spawning enemies
        for (int i = 0; i < 3; i++)
        {
            SpawnEnemy();
        }

        StartCoroutine(EnemiesThrowAI());
    }

    private void Initialize()
    {
        foreach (GameObject current in enemiesToSpawn)
        {
            GameObject enemy = Instantiate(current, spawnPoint.position, spawnPoint.rotation, transform);
            enemy.GetComponent<Enemy>().SetBoundaries(leftBoundary, rightBoundary);
            enemy.SetActive(false);
            spawnedEnemies.Add(enemy);
        }
    }

    public static void SpawnEnemy()
    {
        int i = Random.Range(0, sharedInstace.spawnedEnemies.Count);
        while (sharedInstace.spawnedEnemies[i].activeInHierarchy)
        {
            i = Random.Range(0, sharedInstace.spawnedEnemies.Count);
        }
        
        //Get enemy and set up begining position
        GameObject enemy = sharedInstace.spawnedEnemies[i];
        enemy.transform.position = sharedInstace.spawnPoint.position;
        enemy.SetActive(true);
        
        //Get random end position within boundaries
        float newPosX = Random.Range(sharedInstace.leftBoundary.position.x, sharedInstace.rightBoundary.position.x);
        sharedInstace.StartCoroutine(enemy.GetComponent<Enemy>().EnemyAppear(newPosX));
        
        sharedInstace.activeEnemies.Add(enemy);
    }

    public static void DespawnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        sharedInstace.activeEnemies.Remove(enemy);
    }

    private IEnumerator EnemiesThrowAI()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeForThrow);
            activeEnemies[Random.Range(0,activeEnemies.Count)].GetComponent<Enemy>().ThrowBall();
        }
    }
}
