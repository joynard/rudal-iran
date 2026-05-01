using System.Collections.Generic;
using UnityEngine;

public class Rudal : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 20f;
    public float maxSpeed = 10f;
    public float waypointSatisfaction = 0.3f;
    public float pathUpdateInterval = 0.1f;

    private Pathfinding pathfinder;
    private List<GridNode> path;
    private Rigidbody2D rb;
    private int currentWaypoint = 0;

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
        if (path == null || currentWaypoint >= path.Count) return;

        // Steering Seek Along Path
        Vector2 direction = (path[currentWaypoint].worldPosition - (Vector2)transform.position);

        if (direction.magnitude < waypointSatisfaction)
        {
            currentWaypoint++;
        }

        rb.AddForce(direction.normalized * moveSpeed);
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