using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Helper flag to get pause state
    /// </summary>
    public static bool isGamePaused = false;

    public GameObject pauseMenuUI;

    public SceneChanger sceneChanger;
  
    
    /// <summary>
    /// Method to be given to XRRig OnButtonLongPressed event
    /// </summary>
    public void TriggerPause()
    {
        Debug.Log("PAUSE: " + isGamePaused);
        if (isGamePaused)
            Resume();
        else
            Pause();
    }

    /// <summary>
    /// Disable pause and resume game
    /// </summary>
    public void Resume()
    {
        // TODO: Activate torch light
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    
    /// <summary>
    /// Pause game
    /// </summary>
    void Pause()
    {
        // TODO: Find a way to deactivate torch light
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    /// <summary>
    /// Load Menu scene
    /// </summary>
    public void LoadMenu()
    {
        isGamePaused = false;
        sceneChanger.Menu();
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Exit game
    /// </summary>
    public void Exit()
    {
        sceneChanger.ExitGame();
    }
}
