using UnityEngine;

public class DroneDestroy : MonoBehaviour
{
    public float speed = 5f;
    public float deadZone = -15f;
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DroneAI[] allDrones = FindObjectsByType<DroneAI>(FindObjectsSortMode.None);

            foreach (DroneAI drone in allDrones)
            {
                Destroy(drone.gameObject);
            }

            Destroy(gameObject);
        }
    }
}
