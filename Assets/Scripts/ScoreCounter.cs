using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private float score;

    public void AddScoreValue(float value)
    {
        score += value;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

}
