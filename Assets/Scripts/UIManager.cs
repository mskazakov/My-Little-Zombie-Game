using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    public void Start()
    {
        if (MainManager.Instance != null)
        {
            highScoreText.text = "High Score:\n" + MainManager.Instance.highScore;
            highScoreText.gameObject.SetActive(true);
        }
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }
}
