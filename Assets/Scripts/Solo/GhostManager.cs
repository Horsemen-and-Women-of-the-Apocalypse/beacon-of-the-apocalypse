using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using System.Timers;

public class GhostManager : MonoBehaviour, ITargetable {

    public int timing = 3;

    private Timer timer;

    public void OnEnter() {
        timer = new Timer(timing * 1000);
        timer.Elapsed += OnTimedEvent;
        timer.AutoReset = false;
        timer.Enabled = true;
    }

    public void OnExit() {
        timer.Stop();
    }

    private static void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        Debug.Log("GhostCatched");
        GameObject.Destroy(gameObject);
    }
}
