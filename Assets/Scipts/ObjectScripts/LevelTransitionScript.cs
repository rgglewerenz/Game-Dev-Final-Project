using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionScript : MonoBehaviour
{
    private GameSaveManager SaveManager;

    // Start is called before the first frame update
    void Start()
    {
        SaveManager = FindObjectOfType<GameSaveManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           SaveManager.SaveGame("AutoSave");
        }
    }

}
