using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteController : MonoBehaviour
{

    private List<Transform> waypoints = new List<Transform>();

    [SerializeField]
    private float moveSpeed = 2f;

    [SerializeField]
    private int wayPointQuantity = 16;

    [SerializeField]
    private GameObject moveableObject;

    [SerializeField]
    private GameObject wayPointPrefab;

    private int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < wayPointQuantity; i++)
        {
            CreateWayPoint(i);
        }

        // Set position of Enemy as position of the first waypoint
        moveableObject.transform.position = waypoints[waypointIndex].transform.position;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void CreateWayPoint(int index)
    {
        GameObject wayPoint = Instantiate<GameObject>(wayPointPrefab);


        float y = Mathf.Ceil((index + 2) / 3.0f);
        float x = 0;
        if((index) % 3 != 0)
        {
            if (Mathf.Ceil((index + 1) / 3.0f) % 2 == 0) x = 2; else x = -2;
        }

        wayPoint.transform.position = new Vector3(transform.position.x + x, transform.position.y + y * -0.1f, 0.0f);

        waypoints.Add(wayPoint.transform);
    }

    private void Move()
    {
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Count - 1)
        {

            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            moveableObject.transform.position = Vector2.MoveTowards(moveableObject.transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (moveableObject.transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
    }
}
