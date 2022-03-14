using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Controller;
using Common.Item;
using Common;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BatteryEvent : UnityEvent<bool> { }

[Serializable]
public class FlashEvent : UnityEvent<bool> { }

[Serializable]
public class SonarEvent : UnityEvent<bool> { }

public class Inventory : MonoBehaviour
{
    private AItem[] inventory;

    public BatteryEvent batteryEvent;
    public FlashEvent flashEvent;
    public SonarEvent sonarEvent;

    void Start()
    {
        inventory = new AItem[3];

        StartCoroutine(Init());
    }

    IEnumerator Init()
    {

        yield return new WaitForSeconds(1);

        GameObject.Find("Flashlight(Clone)")?.GetComponentInChildren<Flashlight>()?.onItemCatching.AddListener(Catch);

        Debug.Log(GameObject.Find("Flashlight(Clone)")?.GetComponentInChildren<Flashlight>());

        yield return null;
    }

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
                if (inventory[1] != null)
                {
                    // TODO Consume
                    sonarEvent.Invoke(false);
                    inventory[2] = null; 
                }
                break;
        }
    }
}
