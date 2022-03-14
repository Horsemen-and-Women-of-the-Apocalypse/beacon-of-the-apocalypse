using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public class Clock : MonoBehaviour
{
    // Wait for n seconds then send event
    public float waitTime = 5;
    public UnityEvent onTimeEnd;

    void Start()
    {
        StartCoroutine(WaitForSeconds());
    }

    IEnumerator WaitForSeconds()
    {
       yield return new WaitForSeconds(waitTime);
    
       // Send event
       onTimeEnd.Invoke();

    }
}
