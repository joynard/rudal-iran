using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    public GameObject[] dronePrefabs;
    public float spawnRate = 5f;
    private float timer = 0f;

    public float minY = -3f;
    public float maxY = 3f;

    void Start()
    {
        SpawnDrone();
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
            SpawnDrone();
            timer = 0f;
        }
    }

    void SpawnDrone()
    {
        if (dronePrefabs.Length == 0) return;
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0);
        int randomIndex = Random.Range(0, dronePrefabs.Length);
        Instantiate(dronePrefabs[randomIndex], spawnPosition, Quaternion.identity);
    }
}
