using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public int GetCurrentLevel()
    {
        var activeScene = SceneManager.GetActiveScene();
        var name = activeScene.name;

        if (name.StartsWith("Level"))
        {
            var levelNumberString = name.Substring(5);
            if (int.TryParse(levelNumberString, out int levelNumber))
            {
                return levelNumber;
            }
        }
        return -1;
    }

    public void LoadLevel(int levelNumber)
    {
        string sceneName = "Level" + levelNumber;
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadCurrentLevel()
    {
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}