using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class DoneEvent : UnityEvent { }

[Serializable]
public class TickEvent : UnityEvent { }

public class Timer : MonoBehaviour {

    public Text text;

    public int count = 5;
    public float interval = 1;

    public DoneEvent @event;
    public TickEvent tickEvent;

    void Start() {
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown() {
        for (var i = count; i >= 0; i--) {
            text.text = i.ToString();
            tickEvent.Invoke();

            yield return new WaitForSeconds(interval);
        }

        @event.Invoke();
    }
}
