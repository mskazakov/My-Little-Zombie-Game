using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public TMP_InputField playerNameInputField;
    public GameObject nameInputPanel;

    public void Start()
    {
        highScoreText.text = "High Score:\n" + MainManager.Instance.highScore + "\n" + "by " + MainManager.Instance.highScorePlayerName;
        highScoreText.gameObject.SetActive(true);
    }

    public void NameInputActivator()
    {
        nameInputPanel.gameObject.SetActive(true);
    }

    public void NameInputDeactivator()
    {
        nameInputPanel.gameObject.SetActive(false);
    }

    public void LoadMainScene()
    {
        MainManager.Instance.playerName = playerNameInputField.text;
        SceneManager.LoadScene(1);
    }
}
