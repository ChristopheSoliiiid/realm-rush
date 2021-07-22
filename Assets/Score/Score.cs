using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    int score = 0;

    void Awake()
    {
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    public void IncreaseScore(int amount)
    {
        score += Mathf.Abs(amount);

        UpdateScoreText();
    }
}
