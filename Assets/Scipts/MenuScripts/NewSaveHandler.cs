using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewSaveHandler : MonoBehaviour
{
    public TMP_InputField NewName;

    public SceneHandler sceneHandler;

    public GameSaveManager SaveManager;

    void Start()
    {
        sceneHandler = GetComponent<SceneHandler>();
        SaveManager = GetComponent<GameSaveManager>();
    }

    public void CreateNewSave()
    {
        string saveName = NewName.text;
        if (!string.IsNullOrEmpty(saveName))
        {
            // Implement save creation logic here

            SaveManager.CreateNewSave(saveName);
            sceneHandler.LoadLevel(1); // Load the first level

        }
        else
        {
            return;
        }
    }

}
