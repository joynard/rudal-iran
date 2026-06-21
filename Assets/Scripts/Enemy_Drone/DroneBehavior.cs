using UnityEngine;

public class DroneAI : MonoBehaviour
{
    public enum DroneType { TipeA_Sight, TipeB_Hearing, TipeC_Gabungan }
    public DroneType type;

    public float fastSpeed = 4f;
    public float slowSpeed = 2f;
    public float idleSpeed = 1f;
    public float patrolRange = 2f;

    private DroneSensor sensor;
    private SpriteRenderer spriteRenderer;
    private float startY;
    private bool movingUp = true;

    void Start()
    {
        sensor = GetComponent<DroneSensor>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startY = transform.position.y;
    }

    void Update()
    {
        bool canSee = (type == DroneType.TipeA_Sight || type == DroneType.TipeC_Gabungan) && sensor.targetSensed;
        bool canHear = (type == DroneType.TipeB_Hearing || type == DroneType.TipeC_Gabungan) && sensor.soundSensed;

        if (canSee)
        {
            spriteRenderer.color = Color.red;
            Vector2 direction = (sensor.targetGO.position - transform.position).normalized;
            transform.right = -direction;
            transform.position += (Vector3)(direction * fastSpeed * Time.deltaTime);
        }
        else if (canHear)
        {
            spriteRenderer.color = Color.yellow;
            Vector2 direction = (sensor.soundGO.position - transform.position).normalized;
            transform.right = -direction;
            transform.position += (Vector3)(direction * slowSpeed * Time.deltaTime);
        }
        else
        {
            spriteRenderer.color = Color.white;
            transform.rotation = Quaternion.identity;
            IdlePatrol();
        }
    }

    void IdlePatrol()
    {
        if (movingUp)
        {
            transform.position += Vector3.up * idleSpeed * Time.deltaTime;
            if (transform.position.y > startY + patrolRange) movingUp = false;
        }
        else
        {
            transform.position += Vector3.down * idleSpeed * Time.deltaTime;
            if (transform.position.y < startY - patrolRange) movingUp = true;
        }
    }
}