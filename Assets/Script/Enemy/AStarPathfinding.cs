using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AStarPathfinding : MonoBehaviour
{
    public float randomRadius;
    public float speed = 2;
    public float nextWaypointDistance;
    public bool reachedEndOfPath;
    [HideInInspector]
    public Vector3 nextTargetPosition;
    Seeker seeker;
    Animator animator;
    Path path;
    int currentWaypoint = 0;
    Vector3 targetLastPosition;
    void Start()
    {
        seeker = GetComponent<Seeker>();
        animator = GetComponent<Animator>();
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    public void UpdatePath(Vector2 targetPosition)
    {
        if (Vector2.Distance(targetPosition, targetLastPosition) > 1)
        {
            targetLastPosition = targetPosition;
            seeker.StartPath(transform.position, targetPosition, OnPathComplete);
        }
    }
    public void RandomPath()
    {
        var point = Random.insideUnitSphere * randomRadius;

        //point.z = 0;
        point += transform.position;

        UpdatePath(point);
    }
    public void NextTarget()
    {
        if (path == null)
        {
            return;
        }
        reachedEndOfPath = false;

        float distanceToWaypoint;
        while (true)
        {
            distanceToWaypoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distanceToWaypoint < nextWaypointDistance)
            {
                if (currentWaypoint + 1 < path.vectorPath.Count)
                {
                    currentWaypoint++;
                }
                else
                {
                    reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }
        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distanceToWaypoint / nextWaypointDistance) : 1f;
        if (!reachedEndOfPath)
        {
            nextTargetPosition = path.vectorPath[currentWaypoint];
            Vector3 dir = (nextTargetPosition - transform.position).normalized;
            Vector3 velocity = dir * speed * speedFactor;
            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            path = null;
        }

    }
}
