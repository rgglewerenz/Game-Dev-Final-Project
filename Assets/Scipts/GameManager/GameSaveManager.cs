using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public Transform PlayerSpawn;

    public GameObject PlayerPrefab;

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
