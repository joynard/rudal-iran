using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Mengecek apakah yang menabrak ScoreZone ini adalah Burung/Player
        if (collision.CompareTag("Player"))
        {
            // Panggil GameManager untuk menambahkan 1 poin
            GameManager.instance.AddScore(1);
        }
    }
}
