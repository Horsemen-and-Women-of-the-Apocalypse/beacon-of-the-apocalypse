using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Item;

public class Inventory : MonoBehaviour
{
    private AItem[] inventory;

    void Start()
    {
        inventory = new AItem[3];
    }

    public void Catch(AItem item)
    {
        var type = item.GetType();

        Debug.Log(type + " catched");

        if (type is BatteryItem)
        {
            inventory[0] = item;
        } else if(type is FlashItem)
        {
            inventory[1] = item;
        } else
        {
            inventory[2] = item;
        }
    }

    public void Use(int index)
    {
        switch(index)
        {
            case 0: inventory[0] = null; break; // Battery used
            case 1: inventory[1] = null; break; // Flash used
            case 2: inventory[2] = null; break; // Sonar used
            default: break;
        }
    }
}
