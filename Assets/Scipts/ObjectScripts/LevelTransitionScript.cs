using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionScript : MonoBehaviour
{
    private GameSaveManager SaveManager;
    private SceneHandler SceneHandler;

    public bool TransitionToNextLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        SaveManager = FindObjectOfType<GameSaveManager>();
        SceneHandler = FindObjectOfType<SceneHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (TransitionToNextLevel)
            {
                SaveManager.SaveGame(GameSaveManager.CurrentSaveName);
                SceneHandler.LoadLevel(SceneHandler.GetCurrentLevel() + 1);
                return;
            }

           SaveManager.SaveGame("AutoSave");
        }
    }

}
