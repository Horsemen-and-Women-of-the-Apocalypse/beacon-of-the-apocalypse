using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

/// <summary>
/// Class to update flashlight ui batterie level
/// </summary>
public class BatteryLevel : MonoBehaviour
{
    /// <summary>
    /// Flashlight object
    /// </summary>
    public Flashlight flashlight;

    // Start is called before the first frame update
    void Start()
    {
        flashlight.onBatteryChange.AddListener(UpdateBattery);
    }

    /// <summary>
    /// Called when onBatteryChange event is triggered
    /// </summary>
    /// <param name="battery"></param>
    public void UpdateBattery(float battery)
    {
        gameObject.GetComponent<Text>().text = battery + "%";
    }
}
