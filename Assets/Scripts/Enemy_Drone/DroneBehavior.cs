using UnityEngine;

public class DroneAI : MonoBehaviour
{
    public enum DroneType { TipeA_Sight, TipeB_Hearing, TipeC_Gabungan }
    public DroneType type;

    public float fastSpeed = 4f;
    public float slowSpeed = 2f;
    public float idleSpeed = 1f;
    public float scrollSpeed = 3f;
    
    public float patrolRange = 2f;
    public float deadZone = -15f;

    public ParticleSystem attackAura;
    private bool isAttacking = false;

    private DroneSensor sensor;
    private SpriteRenderer spriteRenderer;
    private float startY;
    private bool movingUp = true;

    public AudioClip destroySound;

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
            if (!isAttacking && attackAura != null)
            {
                attackAura.Play();
                isAttacking = true;
            }
        }
        else if (canHear)
        {
            spriteRenderer.color = Color.yellow;
            Vector2 direction = (sensor.soundGO.position - transform.position).normalized;
            transform.right = -direction;
            transform.position += (Vector3)(direction * slowSpeed * Time.deltaTime);
            if (!isAttacking && attackAura != null)
            {
                attackAura.Play();
                isAttacking = true;
            }
        }
        else
        {
            spriteRenderer.color = Color.white;
            transform.rotation = Quaternion.identity;
            IdlePatrol();

            if (isAttacking && attackAura != null)
            {
                attackAura.Stop();
                isAttacking = false;
            }
        }
        if (transform.position.x < deadZone)
        {
            Destroy(gameObject);
        }
    }

    void IdlePatrol()
    {
        Vector3 movement = Vector3.left * scrollSpeed * Time.deltaTime;

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
        transform.position += movement;
    }

    private void OnDestroy()
    {
        if (destroySound != null)
        {
            AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position);
        }
    }
}