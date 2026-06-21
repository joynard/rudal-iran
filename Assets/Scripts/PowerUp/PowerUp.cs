using UnityEngine;

public class PowerUpPlayer : MonoBehaviour
{
    public float speed = 5f;
    public float deadZone = -15f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Rudal[] allMissiles = FindObjectsByType<Rudal>(FindObjectsSortMode.None);

            foreach (Rudal missile in allMissiles)
            {
                if (gameObject.CompareTag("Shield"))
                {
                    missile.ChangeState(Rudal.MissileState.Flee);
                }
                else if (gameObject.CompareTag("Potion"))
                {
                    missile.ChangeState(Rudal.MissileState.Wander);
                }
            }
            if (gameObject.CompareTag("Destroyer"))
            {
                // Cari semua musuh yang punya script DroneAI di layar
                DroneAI[] allDrones = FindObjectsByType<DroneAI>(FindObjectsSortMode.None);

                // Hancurkan mereka semua satu per satu
                foreach (DroneAI drone in allDrones)
                {
                    Destroy(drone.gameObject);
                }
            }

            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.position += Vector3.left * speed * GameManager.instance.gameSpeedMultiplier * Time.deltaTime;
        //transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}