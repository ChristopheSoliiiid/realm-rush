using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI scoreEndScreenText;

    int score = 0;

    void Awake()
    {
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
        scoreEndScreenText.text = $"Your score is : {score}";
    }

    public void IncreaseScore(int amount)
    {
        score += Mathf.Abs(amount);

        UpdateScoreText();
    }
}
