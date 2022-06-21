using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manage the display of the score (not finished)
/// </summary>
public class ScoreText : MonoBehaviour
{
    public int score;
    public TMP_Text textScore;
    public int combo;

    public static ScoreText instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        combo = 1;
    }

    // Update is called once per frame
    void Update()
    {
        textScore.text = "Score : " + score;
    }

    public void IncrementScore(int scoreadd)
    {
        score += (scoreadd * combo);
        combo++;
    }

    public void LoosePoints(int scoreloose)
    {
        score -= scoreloose;
        combo = 1;
    }
}
