using UnityEngine;

public class RudalAI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum RudalState { Attack, Dodge, Confused }
    public RudalState currentState = RudalState.Attack;

    public Transform player;
    public float speed = 4f;

    private Vector2 wanderDirection;
    public float wanderInterval = 1f;
    private float wanderTimer;
    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }
        PickNewWanderDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        switch (currentState)
        {
            case RudalState.Attack:
                SteeringSeek();
                break;
            case RudalState.Dodge:
                SteeringFlee();
                break;
            case RudalState.Confused:
                Wandering();
                break;
        }
    }
    void SteeringSeek()
    {
        // Rumus Seek = (Posisi Target - Posisi Sendiri).normalized
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        RotateTowards(direction);
    }

    // 2. Steering Flee (Menjauhi Target saat Shield aktif)
    void SteeringFlee()
    {
        Vector2 direction = (transform.position - player.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        RotateTowards(direction);
    }

    // 3. Wandering (Bergerak acak/kacau saat Signal Jammer aktif)
    void Wandering()
    {
        wanderTimer -= Time.deltaTime;
        if (wanderTimer <= 0)
        {
            PickNewWanderDirection();
            wanderTimer = wanderInterval; 
        }

        transform.Translate(wanderDirection * speed * Time.deltaTime, Space.World);
        RotateTowards(wanderDirection);
    }

    void PickNewWanderDirection()
    {
        float randomAngle = Random.Range(0f, 360f); // Sudut acak 0-360 derajat

        // Ubah derajat ke Radian, lalu masukkan ke rumus lingkaran X(Cos) dan Y(Sin)
        wanderDirection = new Vector2(
            Mathf.Cos(randomAngle * Mathf.Deg2Rad),
            Mathf.Sin(randomAngle * Mathf.Deg2Rad)
        ).normalized;
    }

    void RotateTowards(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void ChangeState(RudalState newState)
    {
        currentState = newState;
    }
}
