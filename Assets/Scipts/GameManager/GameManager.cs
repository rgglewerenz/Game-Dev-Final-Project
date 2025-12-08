using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return GameObject.FindAnyObjectByType<GameManager>(); } }

    public GameObject PlayerUI;
    public GameObject PauseUI;
    public GameObject GameOverUI;

    public TMP_Text playerHealthText;
    public TMP_Text playerAmmoCount;

    public bool gameOver = false;

    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void Pause()
    {
        if (gameOver)
            return;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        PlayerUI.SetActive(false);
        PauseUI.SetActive(true);
    }

    public void Resume()
    {
        if(gameOver)
            return;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        PlayerUI.SetActive(true);
        PauseUI.SetActive(false);
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        PlayerUI.SetActive(false);
        PauseUI.SetActive(false);
        GameOverUI.SetActive(true);
    }


    public void SetPlayerHealthText(string text)
    {
        playerHealthText.text = text;
    }
    public void SetPlayerAmmoText(string text)
    {
        playerAmmoCount.text = text;
    }

}
