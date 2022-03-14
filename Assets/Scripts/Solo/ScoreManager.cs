using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Seralizable]
public class WinEvent : UnityEvent<bool> { }

public class ScoreManager : MonoBehaviour {
    private int score = 0;

    public int win = 5;

    public WinEvent winEvent;

    public void Add(int score) {
        this.score += score;
    }

    public int GetScore() {
        return this.score;
    }

    void Update()
    {
        if(score >= win)
        {
            winEvent.invoke(true);
        }
    }
}