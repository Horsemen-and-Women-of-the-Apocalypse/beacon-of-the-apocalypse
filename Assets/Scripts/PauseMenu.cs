using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Flag to get pause state
    /// </summary>
    public static bool isGamePaused = false;

    public GameObject pauseMenuUI;

    public SceneChanger sceneChanger;
    
    /// <summary>
    /// Switch on/off existing camera light
    /// Otherwise, enemies are still visible
    /// </summary>
    public Light cameraLight = null;

    public Flashlight flashlight = null;

    /// <summary>
    /// Must be the same as set Skybox
    /// </summary>
    private Material _skybox;

    private void Start()
    {
        _skybox = RenderSettings.skybox;
    }
    
    /// <summary>
    /// Method to be given to XRRig OnButtonLongPressed event
    /// </summary>
    public void TriggerPause()
    {
        if (isGamePaused)
            Resume();
        else
            Pause();
    }

    private void TurnOnOffCameraLight()
    {
        if (cameraLight) cameraLight.enabled = !cameraLight.enabled;
    }

    private void TurnOnOffFlashlight()
    {
        if (!flashlight)
        {
            GameObject.Find("Flashlight(Clone)")?.GetComponentInChildren<Flashlight>()?.TurnOnOff();
            return;
        }
        flashlight.TurnOnOff();
    }

    /// <summary>
    /// Disable pause and resume game
    /// </summary>
    public void Resume()
    {
        TurnOnOffCameraLight();
        TurnOnOffFlashlight();
        RenderSettings.skybox = _skybox;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }
    
    /// <summary>
    /// Pause game
    /// </summary>
    void Pause()
    {
        TurnOnOffCameraLight();
        TurnOnOffFlashlight();
        RenderSettings.skybox = (null);
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
