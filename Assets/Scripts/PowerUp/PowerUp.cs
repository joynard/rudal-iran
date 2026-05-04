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

            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }
}