using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _bossPrefab;
    [SerializeField]
    private GameObject _extraLife;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerUps;

    private bool _stopSpawning;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(SpawnBossRoutine());
        StartCoroutine(SpawnExtraLifeRoutine());
        Debug.Log("StartSpawning");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while(_stopSpawning == false)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-9, 9), 8, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator SpawnBossRoutine()
    {
        yield return new WaitForSeconds(35.0f);

        while (_stopSpawning == false)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-9, 9), 10, 0);
            GameObject newBoss = Instantiate(_bossPrefab, spawnPosition, Quaternion.identity);
            newBoss.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(10.0f);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(3, 7));
            Vector3 spawnPosition = new Vector3(Random.Range(-9, 9), 8, 0);
            int randomPowerUp = Random.Range(0, 3);
            GameObject currentPowerUp= Instantiate(powerUps[randomPowerUp], spawnPosition, Quaternion.identity);
            currentPowerUp.transform.parent = _enemyContainer.transform;
        }
    }

    IEnumerator SpawnExtraLifeRoutine()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(30.0f);
            Vector3 spawnPosition = new Vector3(Random.Range(-9, 9), 8, 0);
            GameObject extraLife = Instantiate(_extraLife, spawnPosition, Quaternion.identity);
            extraLife.transform.parent = _enemyContainer.transform;
        }
        
    }
    
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
