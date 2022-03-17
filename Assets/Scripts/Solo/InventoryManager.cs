using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used to manage inventory UI
/// </summary>
public class InventoryManager : MonoBehaviour
{
    // Instance of inventory
    private Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("LeftHand Controller").GetComponent<Inventory>();

        inventory.batteryEvent.AddListener(UpdateUIBattery);
        inventory.flashEvent.AddListener(UpdateUIFlash);
        inventory.sonarEvent.AddListener(UpdateUISonar);
    }

    /// <summary>
    /// Update battery UI
    /// </summary>
    /// <param name="status"></param>
    private void UpdateUIBattery(bool status)
    {
        string UIValue = "False";

        if (status) UIValue = "True";

        GameObject.Find("Battery").GetComponent<Text>().text = UIValue;
    }

    /// <summary>
    /// Update flash UI
    /// </summary>
    /// <param name="status"></param>
    private void UpdateUIFlash(bool status)
    {
        string UIValue = "False";

        if (status) UIValue = "True";

        GameObject.Find("Flash").GetComponent<Text>().text = UIValue;
    }

    /// <summary>
    /// Update sonar UI
    /// </summary>
    /// <param name="status"></param>
    private void UpdateUISonar(bool status)
    {
        string UIValue = "False";

        if (status) UIValue = "True";

        GameObject.Find("Sonar").GetComponent<Text>().text = UIValue;
    }
}
