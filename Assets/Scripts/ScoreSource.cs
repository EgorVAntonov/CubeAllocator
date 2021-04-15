using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSource : MonoBehaviour
{
    private ScoreCounter counter;
    [SerializeField] private float scoreValue;

    private void Awake()
    {
        counter = FindObjectOfType<ScoreCounter>();
    }

    public void GetScore(float multiplicator)
    {
        counter.AddScoreValue(scoreValue * multiplicator);
    }
}
