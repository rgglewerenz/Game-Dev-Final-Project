using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject NewSaveUI;
    public GameObject SaveUI;

    void Start()
    {
        MainMenuUI.SetActive(true);
        NewSaveUI.SetActive(false);
        SaveUI.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void NewGame()
    {
        MainMenuUI.SetActive(false);
        NewSaveUI.SetActive(true);
        SaveUI.SetActive(false);
    }

    public void LoadSave()
    {
        MainMenuUI.SetActive(false);
        NewSaveUI.SetActive(false);
        SaveUI.SetActive(true);
    }

}
