using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory.batteryEvent.AddListener(UpdateUIBattery);
        inventory.flashEvent.AddListener(UpdateUIFlash);
        inventory.sonarEvent.AddListener(UpdateUISonar);
    }

    private void UpdateUIBattery(bool status)
    {
        string UIValue = "False";

        if (status) UIValue = "True";

        GameObject.Find("Battery").GetComponent<Text>().text = UIValue;
    }

    private void UpdateUIFlash(bool status)
    {
        string UIValue = "False";

        if (status) UIValue = "True";

        GameObject.Find("Flash").GetComponent<Text>().text = UIValue;
    }

    private void UpdateUISonar(bool status)
    {
        string UIValue = "False";

        if (status) UIValue = "True";

        GameObject.Find("Sonar").GetComponent<Text>().text = UIValue;
    }
}
