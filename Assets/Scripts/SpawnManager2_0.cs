using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager2_0 : MonoBehaviour
{
    public enum WaveState { WAITING, SPAWNING, COUNTING };

    GameManager gameManagerScript;

    public GameObject[] zombies;
    public GameObject bossZombie;
    public GameObject powerup;

    public Vector3[] spawnRange;

    [SerializeField] WaveState state = WaveState.WAITING;

    public int waveNum = 0;
    public float zombieSpawnRate = 0.3f;
    public float waveSpawnRate = 3f;

    public int enemiesCount = 0;
    public int powerupCount;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        waveNum = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == WaveState.COUNTING && gameManagerScript.isGameActive)
        {
            if (!EnemyStillAlive())
            {
                state = WaveState.WAITING;
            } else
            {
                return;
            }
        } 
        
        if (state == WaveState.WAITING && waveSpawnRate <= 0 && gameManagerScript.isGameActive)
        {
            StartCoroutine(SpawnWave());

            waveSpawnRate = 3f;
        } else if (state == WaveState.WAITING && gameManagerScript.isGameActive)
        {
            waveSpawnRate -= Time.deltaTime;
        }
    }

    public IEnumerator SpawnWave()
    {
        state = WaveState.SPAWNING;
        if (waveNum > 3 && !PowerupStillExists()) SpawnPowerup();
        if ((waveNum % 5) == 0)
        {
            SpawnBossZombie();
        }
        else
        {
            for (int i = 0; i < (waveNum + Mathf.RoundToInt(waveNum/2)); i++)
            {
                if (gameManagerScript.isGameActive)
                {
                    SpawnZombie();
                    
                    yield return new WaitForSeconds(zombieSpawnRate);
                }
            }
        }

        waveNum++;
        state = WaveState.COUNTING;
        yield break;
    }    
    public void SpawnZombie()
    {
        spawnRange = new Vector3[4];

        float spawnPosZ = Random.Range(5.0f, 7.0f);
        float spawnPosX01 = Random.Range(-13.0f, 13.0f);
        spawnRange[0] = new Vector3(spawnPosZ, 0.5f, spawnPosX01);
        spawnRange[1] = new Vector3(-spawnPosZ, 0.5f, spawnPosX01);

        float spawnPosZ23 = Random.Range(-7.0f, 7.0f);
        float spawnPosX = Random.Range(11.0f, 13.0f);
        spawnRange[2] = new Vector3(spawnPosZ23, 0.5f, spawnPosX);
        spawnRange[3] = new Vector3(spawnPosZ23, 0.5f, -spawnPosX);

        int spawnNum = Random.Range(0, spawnRange.Length);
        int zombieNum = Random.Range(0, zombies.Length);

        Instantiate(zombies[zombieNum], spawnRange[spawnNum], transform.rotation);
        enemiesCount++;
        Debug.Log($"One more zombie spawned. There are {enemiesCount} zombies alive");
    }

    public void SpawnBossZombie()
    {
        spawnRange = new Vector3[4];

        float spawnPosZ = Random.Range(5.0f, 7.0f);
        float spawnPosX01 = Random.Range(-13.0f, 13.0f);
        spawnRange[0] = new Vector3(spawnPosZ, 1.5f, spawnPosX01);
        spawnRange[1] = new Vector3(-spawnPosZ, 1.5f, spawnPosX01);

        float spawnPosZ23 = Random.Range(-7.0f, 7.0f);
        float spawnPosX = Random.Range(11.0f, 13.0f);
        spawnRange[2] = new Vector3(spawnPosZ23, 1.5f, spawnPosX);
        spawnRange[3] = new Vector3(spawnPosZ23, 1.5f, -spawnPosX);

        int spawnNum = Random.Range(0, spawnRange.Length);

        Instantiate(bossZombie, spawnRange[spawnNum], transform.rotation);
        enemiesCount++;
        Debug.Log($"One more zombie spawned. There are {enemiesCount} zombies alive");
    }

    void SpawnPowerup()
    {
        float spawnPosZ = Random.Range(-4.5f, 4.5f);
        float spawnPosX = Random.Range(-10, 10);

        Instantiate(powerup, new Vector3(spawnPosX, transform.position.y, spawnPosZ), powerup.transform.rotation);
    }

    bool PowerupStillExists()
    {
        powerupCount = FindObjectsOfType<Powerup>().Length;

        if (powerupCount > 0)
        {
            return true;
        }
        return false;
    }

    bool EnemyStillAlive()
    {
        if (enemiesCount == 0)
        {
            return false;
        }
        return true;
    }
}
