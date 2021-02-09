using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class ScoreManager
{
    int score = 0;
    [SerializeField]
    TextMeshProUGUI scoreText;

    public int Score { get { return score; } }

    public void Add(int value)
    {
        score += value;
        UpdateScoreText();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
