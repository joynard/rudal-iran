using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate = 2f;
    private float timer = 0f;
    public float minY = -2f;
    public float maxY = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnPipe();
            timer = 0f; // Reset timer
        }
    }
    void SpawnPipe()
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0);

        // Cetak pipa baru di layar
        Instantiate(pipePrefab, spawnPosition, transform.rotation);
    }
}
