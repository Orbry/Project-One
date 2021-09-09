using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public Color TeamColor;
    
    private string saveFileName = "savefile.json";
    private string saveFilePath;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        saveFilePath = $"{Application.persistentDataPath}/{ saveFileName}";
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadColor();
    }
    
    [System.Serializable]
    internal class SaveData
    {
        public Color TeamColor;
    }
    
    public void SaveColor()
    {
        SaveData data = new SaveData() { TeamColor = TeamColor };
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(saveFilePath, json);
    }
    
    public void LoadColor()
    {
        string path = saveFilePath;
        
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);
            TeamColor = data.TeamColor;
        }
    }
}
