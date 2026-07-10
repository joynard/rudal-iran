using UnityEngine;

public class GenerateDroneDestroyer : MonoBehaviour
{
    public GameObject destroyerPrefab;
    public float spawnInterval = 5f; // Waktu spawn bisa disesuaikan
    public float minY = -4f;
    public float maxY = 4f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnPowerUp();
            timer = 0f;
        }
    }

    void SpawnPowerUp()
    {
        float randomY = Random.Range(minY, maxY);
        Vector2 spawnPos = new Vector2(transform.position.x, randomY);

        GridBlock grid = FindFirstObjectByType<GridBlock>();
        if (grid != null)
        {
            GridNode spawnNode = grid.NodeFromWorldPoint(spawnPos);
            if (spawnNode != null && spawnNode.isWall)
            {
                return; // Batal spawn jika terkena tembok pipa
            }
        }

        Instantiate(destroyerPrefab, spawnPos, Quaternion.identity);
    }
}