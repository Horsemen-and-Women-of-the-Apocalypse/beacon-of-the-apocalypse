using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;

    public void Add(int score)
    {
        this.score += score;
    }

    public int GetScore()
    {
        return this.score;
    }
}
