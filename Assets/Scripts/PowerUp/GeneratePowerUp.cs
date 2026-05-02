using UnityEngine;

public class GeneratePowerUP : MonoBehaviour
{
    public GameObject shieldPrefab, potionPrefab, powerPrefab;
    public float spawnInterval = 3f;
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
            if (spawnNode.isWall)
            {
                return;
            }
        }

        bool changer = true;
        if (changer == true)
        {
            powerPrefab = shieldPrefab;
            changer = false;
        }
        else if (changer == false)
        {
            powerPrefab = potionPrefab;
            changer = true;
        }

        GameObject newPower = Instantiate(powerPrefab, spawnPos, Quaternion.identity);
    }
}