using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manage the display of the score (not finished)
/// </summary>
public class ScoreText : MonoBehaviour
{
    public static int score;
    public TMP_Text textScore;
    public static int combo;

    // Update is called once per frame
    void Update()
    {
        textScore.text = "Score : " + score;
    }

    public static void IncrementScore(int scoreadd)
    {
        score = score + (scoreadd * combo);
    }
}
