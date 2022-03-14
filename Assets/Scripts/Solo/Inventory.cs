using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Controller;
using Common.Item;
using Common;

public class Inventory : MonoBehaviour
{
    private AItem[] inventory;

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
                Debug.Log("Battery");
                if (inventory[0] == null)
                {
                    inventory[0] = item;
                    item.gameObject.SetActiveRecursively(false);
                }
            }
            else if (item is FlashItem)
            {
                if (inventory[1] == null)
                {
                    inventory[1] = item;
                    item.gameObject.SetActiveRecursively(false);
                }
            }
            else
            {
                if (inventory[2] == null)
                {
                    inventory[2] = item;
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
                    inventory[0] = null;
                }
                 break; 
                
            case ETouchPadButton.Right: // Flash used
                if (inventory[1] != null)
                {
                    flashlight.Consume((FlashItem) inventory[1]);
                    inventory[1] = null;
                }
                break; 
            case ETouchPadButton.Left: //  Sonar used  

                inventory[2] = null; break;  
        }
    }
}
