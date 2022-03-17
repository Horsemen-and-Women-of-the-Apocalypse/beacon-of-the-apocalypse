using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Common.Controller;

/// <summary>
/// Class used to access flashlight object instance
/// </summary>
public class FlashLightController : MonoBehaviour
{
    // Menu state
    private bool lastStateMenu = true;

    // Trigger state
    private bool lastStateTrigger = true;

    /// <summary>
    /// Inventory object
    /// </summary>
    public Inventory inventory;

    /// <summary>
    /// Function to call flashligh TurnOnOff function
    /// </summary>
    /// <param name="state"></param>
    public void TurnOnOff(bool state)
    {
        if(!state && lastStateMenu)
        {
            GameObject.Find("Flashlight(Clone)")?.GetComponentInChildren<Flashlight>()?.TurnOnOff();
           
        }
        lastStateMenu = state;
    }

    /// <summary>
    /// Function to call flashligh CatchItems function
    /// </summary>
    /// <param name="state"></param>
    public void CatchItems(bool state)
    {
        if (!state && lastStateTrigger)
        {
            GameObject.Find("Flashlight(Clone)")?.GetComponentInChildren<Flashlight>()?.CatchItems();
        }
        lastStateTrigger = state;
    }

    /// <summary>
    /// Function to call flashligh UseItem function
    /// </summary>
    /// <param name="state"></param>
    public void UseItem(ETouchPadButton button)
    {
        Flashlight flashlight = GameObject.Find("Flashlight(Clone)")?.GetComponentInChildren<Flashlight>();

        inventory.Use(button, flashlight);
    }
}
