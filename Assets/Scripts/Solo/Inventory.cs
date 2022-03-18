using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Controller;
using Common.Item;
using Common;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Battery event
/// </summary>
[Serializable]
public class BatteryEvent : UnityEvent<bool> { }

/// <summary>
/// Flash event
/// </summary>
[Serializable]
public class FlashEvent : UnityEvent<bool> { }

/// <summary>
/// Sonar event
/// </summary>
[Serializable]
public class SonarEvent : UnityEvent<bool> { }

/// <summary>
/// Class to store and use items
/// </summary>
public class Inventory : MonoBehaviour
{
    /// <summary>
    /// Battery event instance
    /// </summary>
    public BatteryEvent batteryEvent;

    /// <summary>
    /// Flash event instance
    /// </summary>
    public FlashEvent flashEvent;

    /// <summary>
    /// Sonar event instance
    /// </summary>
    public SonarEvent sonarEvent;

    // Inventory containing items
    private AItem[] inventory;

    void Start()
    {
        inventory = new AItem[3];

        StartCoroutine(Init());
    }

    /// <summary>
    /// Used to init listener for catch
    /// </summary>
    /// <returns></returns>
    IEnumerator Init()
    {
        yield return new WaitForSeconds(1);

        GameObject.Find("Flashlight(Clone)")?.GetComponentInChildren<Flashlight>()?.onItemCatching.AddListener(Catch);

        yield return null;
    }

    /// <summary>
    /// Function called when an item is catched
    /// </summary>
    /// <param name="items"></param>
    public void Catch(IList<AItem> items)
    {
        foreach(AItem item in items)
        {
            if (item is BatteryItem)
            {
                if (inventory[0] == null)
                {
                    inventory[0] = item;
                    batteryEvent.Invoke(true);
                    item.gameObject.SetActiveRecursively(false);
                }
            }
            else if (item is FlashItem)
            {
                if (inventory[1] == null)
                {
                    inventory[1] = item;
                    flashEvent.Invoke(true);
                    item.gameObject.SetActiveRecursively(false);
                }
            }
            else
            {
                if (inventory[2] == null)
                {
                    inventory[2] = item;
                    sonarEvent.Invoke(true);
                    item.gameObject.SetActiveRecursively(false);
                }
            }
        }
    }

    /// <summary>
    /// Function to use items
    /// </summary>
    /// <param name="button"></param>
    /// <param name="flashlight"></param>
    public void Use(ETouchPadButton button, Flashlight flashlight)
    {
        switch (button)
        {
            case ETouchPadButton.Top: // Battery used
                if(inventory[0] != null)
                {
                    flashlight.Consume((BatteryItem) inventory[0]);
                    batteryEvent.Invoke(false);
                    inventory[0] = null;
                }
                 break; 
                
            case ETouchPadButton.Right: // Flash used
                if (inventory[1] != null)
                {
                    flashlight.Consume((FlashItem) inventory[1]);
                    flashEvent.Invoke(false);
                    inventory[1] = null;
                }
                break; 
            case ETouchPadButton.Left: //  Sonar used 
                if (inventory[2] != null)
                {
                    flashlight.Consume((SonarItem)inventory[2]);
                    sonarEvent.Invoke(false);
                    inventory[2] = null; 
                }
                break;
        }
    }
}
