using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public int highScore;
    public string playerName;
    public string highScorePlayerName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string playerName;
        public string highScorePlayerName;
    }

    public void SaveDataJSON()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.playerName = playerName;
        data.highScorePlayerName = highScorePlayerName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScore = data.highScore;
            playerName = data.playerName;
            highScorePlayerName = data.highScorePlayerName;
        }
    }
}
