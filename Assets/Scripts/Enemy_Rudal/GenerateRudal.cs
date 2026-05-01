using UnityEngine;

public class GenerateRudal : MonoBehaviour
{
    public GameObject missilePrefab;
    public float spawnInterval = 3f;
    public float minY = -4f;
    public float maxY = 4f;
    public Transform targetTransform;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnMissile();
            timer = 0f;
        }
    }

    void SpawnMissile()
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

        GameObject newMissile = Instantiate(missilePrefab, spawnPos, Quaternion.identity);
        newMissile.GetComponent<Rudal>().target = targetTransform;
    }
}