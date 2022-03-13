using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class FlashLightController : MonoBehaviour
{

    private bool lastStateMenu = true;
    private bool lastStateTrigger = true;

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
}
