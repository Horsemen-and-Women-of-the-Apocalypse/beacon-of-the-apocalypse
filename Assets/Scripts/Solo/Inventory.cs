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
            var type = item.GetType();     

            if (type is BatteryItem)
            {
                inventory[0] = item;
                Destroy(item);
            }
            else if (type is FlashItem)
            {
                inventory[1] = item;
                Destroy(item);
            }
            else
            {
                inventory[2] = item;
                Destroy(item);
            }
        }
    }

    public void Use(ETouchPadButton button)
    {
        switch (button)
        {
            case ETouchPadButton.Top: inventory[0] = null; break; // Battery used
            case ETouchPadButton.Right: inventory[1] = null; break; // Flash used
            case ETouchPadButton.Left: inventory[2] = null; break; //  Sonar used    
        }
    }
}
