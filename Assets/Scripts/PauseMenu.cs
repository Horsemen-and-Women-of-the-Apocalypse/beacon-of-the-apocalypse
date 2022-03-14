using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Helper flag to get pause state
    /// </summary>
    public static bool IsGamePaused = false;

    public GameObject pauseMenuUI;

    public SceneChanger sceneChanger;
    
    public void TriggerPause()
    {
        Debug.Log("PAUSE: " + IsGamePaused);
        if (IsGamePaused)
            Resume();
        else
            Pause();
    }

    public void Resume()
    {
        // TODO: Activate torch light
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
    }
    
    void Pause()
    {
        // TODO: Find a way to deactivate torch light
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void LoadMenu()
    {
        IsGamePaused = false;
        sceneChanger.Menu();
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        sceneChanger.ExitGame();
    }
}
