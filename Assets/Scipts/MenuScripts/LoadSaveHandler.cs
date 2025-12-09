using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaveHandler : MonoBehaviour
{
    GameSaveManager saveManager;
    SceneHandler sceneHandler;
    public GameObject ContentArea;
    public GameObject ButtonPrefab;
    int selectedIndex = -1;


    List<string> saveFiles = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        saveManager = GetComponent<GameSaveManager>();
        sceneHandler = GetComponent<SceneHandler>();
        saveFiles = saveManager.GetSaveFiles();
        PopulateLoadData();
    }

    private void PopulateLoadData()
    {
        foreach (Transform child in ContentArea.transform)
            Destroy(child.gameObject);

        for (int i = 0; i < saveFiles.Count; i++)
        {
            int index = i; // VERY IMPORTANT for closures

            GameObject btnObj = Instantiate(ButtonPrefab, ContentArea.transform);

            // Optional: Set the button text
            var text = btnObj.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (text != null)
                text.text = saveFiles[index];

            // Add onClick listener
            var button = btnObj.GetComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(() => SelectItem(index));
        }
    }

    public void SelectItem(int index)
    {
        selectedIndex = index;
    }

    
    public void LoadData()
    {
        if(selectedIndex == -1)
        {
            return;
        }

        var saveName = saveFiles[selectedIndex];

        GameSaveManager.CurrentSaveName = saveName;

        var lastLevel = saveManager.GetSaveLastLevel(saveName);

        var levelNumberString = lastLevel.Substring(5);
        if (int.TryParse(levelNumberString, out int levelNumber))
        {
            sceneHandler.LoadLevel(levelNumber);
        }
    }

}
