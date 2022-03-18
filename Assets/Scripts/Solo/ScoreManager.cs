using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event triggered on winning
/// </summary>
[Serializable]
public class WinEvent : UnityEvent<bool> { }

/// <summary>
/// Class to manage score
/// </summary>
public class ScoreManager : MonoBehaviour {
   
    /// <summary>
    /// Score needed to win
    /// </summary>
    public int win = 50;

    /// <summary>
    /// Instance of WinEvent
    /// </summary>
    public WinEvent winEvent;

    // Default score
    private int score = 0;

    /// <summary>
    /// Add given score to score
    /// </summary>
    /// <param name="score"></param>
    public void Add(int score) {
        this.score += score;
    }

    /// <summary>
    /// Get the score
    /// </summary>
    /// <returns></returns>
    public int GetScore() {
        return this.score;
    }

    void Update()
    {
        if(score >= win)
        {
            winEvent.Invoke(true);
        }
    }
}