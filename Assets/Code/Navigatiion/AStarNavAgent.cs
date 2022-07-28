using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
public class AStarNavAgent : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1;

    public float nextWaypointDistance = 0.01f;
    
    private Vector3 myPosition => transform.position;
    private Path path;
    private Seeker seeker;
    private int currentWayPoint;
    private Vector3 destination;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
    }

    public void GoTo(Vector3 destination)
    {
        this.destination = destination;
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, destination, newPath =>
            {
                if (!newPath.error)
                {
                    this.path = newPath;
                    currentWayPoint = 0;
                }
            });
        }
    }

    private void Update()
    {
        if (path == null)
        {
            return;
        }

        if (currentWayPoint >= path.vectorPath.Count)
        {
            path = null;
            return;   
        }

        Vector3 targetPos = path.vectorPath[currentWayPoint];
        Vector3 directionToTarget = (targetPos - myPosition).normalized;
        Vector3 moveVector = directionToTarget * moveSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        float distanceToTarget = Vector3.Distance(targetPos, myPosition);
        if (distanceToTarget <= nextWaypointDistance)
        {
            currentWayPoint++;
        }

    }
}
