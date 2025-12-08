using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaveManager : MonoBehaviour
{
    public Transform PlayerSpawn;

    public GameObject PlayerPrefab;

    private const string saveFileExtension = ".dat";

    private const string saveFileDirectory = "/Saves/";

    private string GetSaveFilePath(string saveFileName)
    {
        return UnityEngine.Application.dataPath + saveFileDirectory + saveFileName + saveFileExtension;
    }

    private List<string> GetSaveFiles()
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


    private void Start()
    {
        LoadGame();
    }

    public void LoadGame()
    {
        // For now, just spawn the player at the spawn point
        Instantiate(PlayerPrefab, PlayerSpawn.position, PlayerSpawn.rotation);


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


        sb.AppendLine(SceneManager.GetActiveScene().name);
        sb.AppendLine(health.CurrentHealth.ToString());
        sb.AppendLine(gunHandler.SirialzeGunsToString());





        return sb.ToString();
    }

    private void LoadGameData(string saveName)
    {
        string filePath = GetSaveFilePath(saveName);
        if (!File.Exists(filePath))
        {
            Debug.LogError("Save file does not exist: " + filePath);
            return;
        }
        string[] lines = File.ReadAllLines(filePath);
        string sceneName = lines[0];
        int playerHealth = int.Parse(lines[1]);

        var gunData = lines.Skip(2);

        // Load the scene
        SceneManager.LoadScene(sceneName);
        // After scene is loaded, set player health and guns
        var player = GameObject.FindGameObjectWithTag("Player");
        var health = player.GetComponent<PlayerHealthScript>();
        var gunHandler = player.GetComponent<PlayerGunHandler>();
        health.SetHealth(playerHealth);
        gunHandler.LoadGunsFromStrings(gunData);
    }

}
