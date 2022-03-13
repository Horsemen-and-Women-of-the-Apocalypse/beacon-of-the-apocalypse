using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Common.Controller;

public class FlashLightController : MonoBehaviour
{

    private bool lastStateMenu = true;
    private bool lastStateTrigger = true;

    public Inventory inventory;

    public void TurnOnOff(bool state)
    {
        if(!state && lastStateMenu)
        {
            GameObject.Find("Flashlight(Clone)")?.GetComponentInChildren<Flashlight>()?.TurnOnOff();
           
        }
        lastStateMenu = state;
    }

    public void CatchItems(bool state)
    {
        if (!state && lastStateTrigger)
        {
            GameObject.Find("Flashlight(Clone)")?.GetComponentInChildren<Flashlight>()?.CatchItems();
        }
        lastStateTrigger = state;
    }

    public void UseItem(ETouchPadButton button)
    {
        // TODO action on used
        inventory.Use(button);
    }
}
