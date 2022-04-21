using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvingPlatform1 : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;
    [SerializeField] float speed = 1f;
    [SerializeField] float timer;
    [SerializeField] float timeReset;

    void Update()
    {
        if(Vector3.Distance(transform.position,waypoints[currentWaypointIndex].transform.position)<.1f)
        {
            
            timer -= Time.deltaTime;
            if (timer<=0)
            {
                currentWaypointIndex=currentWaypointIndex+1;
                timer = timeReset;
                if (currentWaypointIndex >= waypoints.Length)
                    {
                        currentWaypointIndex = 0;
                        
                    }
                        
            }
            
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
       
    }
}