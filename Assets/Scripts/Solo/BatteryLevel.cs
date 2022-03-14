using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

public class BatteryLevel : MonoBehaviour
{
    public Flashlight flashlight;

    // Start is called before the first frame update
    void Start()
    {
        flashlight.onBatteryChange.AddListener(UpdateBattery);
    }

    // Update is called once per frame
    public void UpdateBattery(float battery)
    {
        gameObject.GetComponent<Text>().text = battery + "%";
    }
}
