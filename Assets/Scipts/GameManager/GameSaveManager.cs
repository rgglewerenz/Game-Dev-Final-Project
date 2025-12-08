using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaveManager : MonoBehaviour
{
    public static string CurrentSaveName = "DefaultSave";

    public Transform PlayerSpawn;

    public GameObject PlayerPrefab;

    SceneHandler SceneHandler;

    GameManager GameManager;

    public bool LoadOnStart = false;

    private const string saveFileExtension = ".dat";

    private const string saveFileDirectory = "/Saves/";

    private string GetSaveFilePath(string saveFileName)
    {
        return UnityEngine.Application.dataPath + saveFileDirectory + saveFileName + saveFileExtension;
    }

    public List<string> GetSaveFiles()
    {
        if (!Directory.Exists(UnityEngine.Application.dataPath + saveFileDirectory))
        {
            Directory.CreateDirectory(UnityEngine.Application.dataPath + saveFileDirectory);
        }
        string[] files = Directory.GetFiles(UnityEngine.Application.dataPath + saveFileDirectory, "*" + saveFileExtension);

        List<string> saveFiles = new List<string>();
        foreach (string file in files)
        {
            saveFiles.Add(Path.GetFileNameWithoutExtension(file));
        }
        return saveFiles;
    }

    public bool SaveFileExists(string saveName)
    {
        string filePath = GetSaveFilePath(saveName);
        return File.Exists(filePath);
    }

    public string GetSaveLastLevel(string saveName)
    {
        string filePath = GetSaveFilePath(saveName);
        if (!File.Exists(filePath))
        {
            Debug.LogError("Save file does not exist: " + filePath);
            return null;
        }
        string[] lines = File.ReadAllLines(filePath);
        return lines[0];
    }

    public void SaveGame(string saveName)
    {
        SaveGameData(saveName);
    }

    public void LoadGame()
    {
        Instantiate(PlayerPrefab, PlayerSpawn.position, PlayerSpawn.rotation);

        LoadGameData(CurrentSaveName);
    }

    void Start()
    {
        SceneHandler = FindObjectOfType<SceneHandler>();
        GameManager = FindObjectOfType<GameManager>();
        if (LoadOnStart)
        {
            LoadGame();
            GameManager.Resume();
        }
    }

    private void SaveGameData(string saveName)
    {
        string filePath = GetSaveFilePath(saveName);
        if(File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        File.Create(filePath).Close();
        File.WriteAllText(filePath, SerializeGameData());
    }
    

    private string SerializeGameData()
    {
        StringBuilder sb = new StringBuilder();

        var player = GameObject.FindGameObjectWithTag("Player");

        var health = player.GetComponent<PlayerHealthScript>();
        var gunHandler = player.GetComponent<PlayerGunHandler>();


        sb.AppendLine($"Level{SceneHandler.GetCurrentLevel() + 1}");
        sb.AppendLine(health.CurrentHealth.ToString());
        sb.AppendLine(gunHandler.SirialzeGunsToString());





        return sb.ToString();
    }

    private void LoadGameData(string saveName)
    {
        CurrentSaveName = saveName;
        string filePath = GetSaveFilePath(saveName);
        if (!File.Exists(filePath))
        {
            Debug.LogError("Save file does not exist: " + filePath);
            return;
        }
        string[] lines = File.ReadAllLines(filePath);
        string sceneName = lines[0];
        int playerHealth = int.Parse(lines[1]);

        var gunData = lines.Skip(2).ToList();

        gunData = gunData.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

        var player = GameObject.FindGameObjectWithTag("Player");
        var health = player.GetComponent<PlayerHealthScript>();
        var gunHandler = player.GetComponent<PlayerGunHandler>();
        health.SetHealth(playerHealth);
        if(gunData.Count != 0)
            gunHandler.LoadGunsFromStrings(gunData);
    }

    public void CreateNewSave(string saveName)
    {
        string filePath = GetSaveFilePath(saveName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        File.Create(filePath).Close();
        File.WriteAllText(filePath, "Level1\n100");
        CurrentSaveName = saveName;
    }

}
