using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static MenuManager Instance;
    public string playerName;
    public InputField inputField;
    public Text bestScoreText;
    private string bestPlayerName;
    private int bestPlayerScore;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SaveData saveData = new SaveData();
        saveData.LoadBestScore();

        bestScoreText.text = $"Name: {saveData.playerName} Score: {saveData.bestScore}";
    }
    
    public void StartGame()
    {
        playerName = inputField.text;
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
 #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
 #else
        Application.Quit();
 #endif
    }
    public class SaveData
    {
        public int bestScore;
        public string playerName;       

        public void LoadBestScore()
        {
            string path = Application.persistentDataPath + "/savefile.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                SaveData data = JsonUtility.FromJson<SaveData>(json);

                bestScore = data.bestScore;
                playerName = data.playerName;
            }
        }
    }
}
