using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesSpawnManager : MonoBehaviour
{
    GameManager gameManagerScript;
    
    public enum SpawnState { SPAWNING, WAITING, COUNTING};
    
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public Transform enemy;
        public int count;
        public float spawnRate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCD;

    private float searchCD = 1f;

    public SpawnState state; // = SpawnState.COUNTING

    private void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (state == SpawnState.WAITING && gameManagerScript.isGameActive)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        
        if (waveCD <= 0 && gameManagerScript.isGameActive)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else if (gameManagerScript.isGameActive)
        {
            waveCD -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {

        state = SpawnState.COUNTING;
        waveCD = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All waves are complete. Looping...");
        } else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCD -= Time.deltaTime;

        if (searchCD <= 0)
        {
            searchCD = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Sapwning wave " + _wave.waveName);
        
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);

            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning enemy" + _enemy.name);

        float spawnPosZ = Random.Range(-7, 7);
        float spawnPosX = Random.Range(-12, 12);

        Instantiate(_enemy, new Vector3(spawnPosX, transform.position.y, spawnPosZ), transform.rotation);
    }
}
