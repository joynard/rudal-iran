using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public float gameSpeedMultiplier = 1f;
    public float speedIncreaseRate = 0.01f;

    public TextMeshProUGUI scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        PlayerPrefs.SetInt("CurrentScore", 0);

        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameSpeedMultiplier += Time.deltaTime * speedIncreaseRate;
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Skor Saat Ini: " + score);

        if (scoreText != null)
        {
            scoreText.text = "Score : " + score.ToString();
        }

        PlayerPrefs.SetInt("CurrentScore", score);

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
