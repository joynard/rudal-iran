using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public float gameSpeedMultiplier = 1f;
    public float speedIncreaseRate = 0.01f;

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
    }
}
