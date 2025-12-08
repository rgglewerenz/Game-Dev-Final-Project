using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (!Directory.Exists(UnityEngine.Application.dataPath + "appdata"))
        {
            Directory.CreateDirectory(UnityEngine.Application.dataPath + "appdata");
        }
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


    

}
