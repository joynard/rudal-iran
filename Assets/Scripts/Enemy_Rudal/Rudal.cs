using System.Collections.Generic;
using UnityEngine;

public class Rudal : MonoBehaviour
{
    public enum MissileState { Seek, Flee, Wander }
    //seek utk homing missile, flee utk terkena powerup shield, wander utk terkena powerup potion
    public MissileState currentState = MissileState.Seek;

    public Transform target;
    public float moveSpeed = 20f;
    public float maxSpeed = 10f;
    public float waypointSatisfaction = 0.3f;
    public float pathUpdateInterval = 0.1f;

    private Pathfinding pathfinder;
    private List<GridNode> path;
    private Rigidbody2D rb;
    private int currentWaypoint = 0;

    private float wanderTimer;
    private Vector2 wanderDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pathfinder = FindFirstObjectByType<Pathfinding>();
        Destroy(gameObject, 5f);
        InvokeRepeating("UpdatePath", 0f, pathUpdateInterval);
    }

    void UpdatePath()
    {
        if (target != null)
            path = pathfinder.FindPath(transform.position, target.position);
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case MissileState.Seek:
                SeekBehavior();
                break;
            case MissileState.Flee:
                FleeBehavior();
                break;
            case MissileState.Wander:
                WanderBehavior();
                break;
        }
    }

    public void ChangeState(MissileState newState)
    {
        currentState = newState;
    }

    void SeekBehavior()
    {
        if (path == null || currentWaypoint >= path.Count) return;

        Vector2 direction = (path[currentWaypoint].worldPosition - (Vector2)transform.position);

        if (direction.magnitude < waypointSatisfaction)
        {
            currentWaypoint++;
        }

        rb.AddForce(direction.normalized * moveSpeed);
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    void FleeBehavior()
    {
        if (target == null) return;

        Vector2 direction = (Vector2)transform.position - (Vector2)target.position;
        rb.AddForce(direction.normalized * moveSpeed);
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    void WanderBehavior()
    {
        wanderTimer -= Time.fixedDeltaTime;
        if (wanderTimer <= 0)
        {
            wanderDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            wanderTimer = 1f;
        }

        rb.AddForce(wanderDirection * moveSpeed);
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}