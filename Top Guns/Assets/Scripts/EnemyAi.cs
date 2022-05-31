using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAi : MonoBehaviour
{
    private Seeker seeker;
    private Rigidbody2D rb;
    private GameObject Player;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    
    private Path path;
    private int currentWaypoint;
    bool reachedEndOfPath = false;

    void Start()
    {
        seeker = gameObject.GetComponent<Seeker>();
        rb = gameObject.GetComponent<Rigidbody2D>();


        
        Player = GameObject.FindGameObjectWithTag("Player");


        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, Player.transform.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        transform.position += ((Vector3)path.vectorPath[currentWaypoint] - transform.position) * 0.3f * Time.fixedDeltaTime;
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        //rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
