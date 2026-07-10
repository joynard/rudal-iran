using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        int currentScore = PlayerPrefs.GetInt("CurrentScore", 0);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (currentScoreText != null)
        {
            currentScoreText.text = "SCORE: " + currentScore.ToString();
        }

        if (highScoreText != null)
        {
            highScoreText.text = "HIGH SCORE: " + highScore.ToString();
        }
    }
}