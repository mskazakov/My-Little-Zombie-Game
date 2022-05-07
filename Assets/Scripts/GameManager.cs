using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    
    public GameObject gameOverScreen;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;
    
    public GameObject titleScreen;
    public TextMeshProUGUI titleText;
    public Button startButton;
    
    public bool isGameActive;

    public int totalScore;
    public int zombieScore = 10;
    public int strongZombieScore = 20;
    public int bossZombieScore = 100;

    public int playerLives = 3;

    WavesSpawnManager wavesSpawnManagerScript;
    SpawnManager2_0 spawnManager2_0;
    
    // Start is called before the first frame update
    void Start()
    {
        wavesSpawnManagerScript = GameObject.Find("SpawnManager").GetComponent<WavesSpawnManager>();
        spawnManager2_0 = GameObject.Find("SpawnManager").GetComponent<SpawnManager2_0>();

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddLives(int value)
    {
        playerLives += value;

        if (playerLives <= 0)
        {
            GameOver();
        }

        livesText.text = "Lives: " + playerLives;
    }

    public void AddScoreZ(int zombieScore)
    {
        totalScore += zombieScore;
        scoreText.text = "Score: " + totalScore;
    }

    public void AddScoreZS(int strongZombieScore)
    {
        totalScore += strongZombieScore;
        scoreText.text = "Score: " + totalScore;
    }
    public void AddScoreB(int bossZombieScore)
    {
        totalScore += bossZombieScore;
        scoreText.text = "Score: " + totalScore;
    }

    public void StartGame()
    {
        playerNameText.gameObject.SetActive(true);
        playerNameText.text = "Player: " + MainManager.Instance.playerName;
        scoreText.gameObject.SetActive(true);
        scoreText.text = "Score: " + totalScore;
        livesText.gameObject.SetActive(true);
        livesText.text = "Lives: " + playerLives;

        isGameActive = true;
        totalScore = 0;
        //spawnManager2_0.WaitingStart();
        //wavesSpawnManagerScript.state = WavesSpawnManager.SpawnState.COUNTING;
        //wavesSpawnManagerScript.waveCD = wavesSpawnManagerScript.timeBetweenWaves;
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        isGameActive = false;
        playerNameText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        livesText.gameObject.SetActive(false);
        finalScoreText.text = "Final score: " + totalScore;

        if (totalScore > MainManager.Instance.highScore)
        {
            MainManager.Instance.highScore = totalScore;
            MainManager.Instance.highScorePlayerName = MainManager.Instance.playerName;
        }

        highScoreText.text = $"High Score: {MainManager.Instance.highScore} by {MainManager.Instance.highScorePlayerName}";

        gameOverScreen.gameObject.SetActive(true);

        MainManager.Instance.SaveDataJSON();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
