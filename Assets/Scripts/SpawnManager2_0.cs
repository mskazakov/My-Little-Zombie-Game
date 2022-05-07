using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager2_0 : MonoBehaviour
{
    public enum WaveState { WAITING, SPAWNING, COUNTING };

    GameManager gameManagerScript;

    public GameObject[] zombies;

    public Vector3[] spawnRange;

    public WaveState state;

    public int waveNum;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnZombie()
    {
        if (gameManagerScript.isGameActive)
        {
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
        }
    }

    public IEnumerator WaveSpawn()
    {
        state = WaveState.SPAWNING;

        for (int i = 0; i < 10; i++)
        {
            SpawnZombie();

            yield return new WaitForSeconds(0.2f);
        }

        StartCoroutine(WaitingState());

        yield break;
    }

    public IEnumerator WaitingState()
    {
        if (gameManagerScript.isGameActive)
        {
            state = WaveState.WAITING;

            yield return new WaitForSeconds(5);

            StartCoroutine(WaveSpawn());

            yield break;
        }
    }

    public void WaitingStart()
    {
        StartCoroutine(WaitingState());
    }

    bool EnemieIsAlive()
    {
        state = WaveState.COUNTING;
        
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            return false;
        } else
        {
            return true;
        }
    }
}
