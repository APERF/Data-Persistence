using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public string playerName;
    public string highScoreName;

    public int currentScore;
    public int highScore;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    [System.Serializable]
    public class SaveData
    {
        public string highScoreName;
        public int highScore;
    }

    public void SaveHighScore(int currentScore, string playerName)
    {
        SaveData data = new SaveData();

        data.highScore = currentScore;
        data.highScoreName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScoreName = data.highScoreName;
            highScore = data.highScore;
        }
    }
}
