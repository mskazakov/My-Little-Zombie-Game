using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    GameManager gameManagerScript;

    public GameObject[] zombies;
    public GameObject zombieBoss;
    
    public GameObject powerup;
    public int powerupCounter;
    
    float spawnDelay = 2;
    float spawnRate = 1;
    float bossDelay = 30;
    float bossSpawnRate = 30;
    float powerupSpawnRate = 10;
    float powerupSpawnDelay = 4;

    public Vector3[] spawnRange;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();

        InvokeRepeating("ZombieSpawner", spawnDelay, spawnRate);
        InvokeRepeating("ZombieOffscreenSpawner", bossDelay, bossSpawnRate);
        InvokeRepeating("PowerupSpawner", powerupSpawnDelay, powerupSpawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        powerupCounter = FindObjectsOfType<Powerup>().Length;
    }

    void ZombieSpawner()
    {
        if (gameManagerScript.isGameActive)
        {
            int zombieNum = Random.Range(0, zombies.Length);
            float spawnPosZ = Random.Range( -7, 7);
            float spawnPosX = Random.Range(-12, 12);

            Instantiate(zombies[zombieNum], new Vector3(spawnPosX, transform.position.y, spawnPosZ), transform.rotation);
        }
    }

    void ZombieOffscreenSpawner()
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

            Instantiate(zombieBoss, spawnRange[spawnNum], transform.rotation);
            Debug.Log("Boss is here!");
        }    
    }

    void PowerupSpawner()
    {
        float spawnPosZ = Random.Range(-4.5f, 4.5f);
        float spawnPosX = Random.Range(-10, 10);

        if (powerupCounter < 1 && gameManagerScript.isGameActive)
        {
            Instantiate(powerup, new Vector3(spawnPosX, transform.position.y, spawnPosZ), powerup.transform.rotation);
        }
    }
}
