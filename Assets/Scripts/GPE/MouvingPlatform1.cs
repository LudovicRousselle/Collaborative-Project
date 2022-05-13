using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvingPlatform1 : RewindableObject
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float timer;
    [SerializeField] private float timeReset;
    
    public bool isRewind;
   

    void Update()
    {

        

        if (GroundButton.onPlatform==false)
        {
            Debug.Log(GroundButton.onPlatform);
            
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f)
            {

                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    currentWaypointIndex = currentWaypointIndex + 1;
                    timer = timeReset;
                    if (currentWaypointIndex >= waypoints.Length)
                    {
                        currentWaypointIndex = 0;


                    }

                }

            }
            else if (isRewind)
            {
                if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f)
                {

                    timer -= Time.deltaTime;
                    if (timer <= 0)
                    {
                        currentWaypointIndex = currentWaypointIndex - 1;
                        timer = timeReset;
                        if (currentWaypointIndex <= waypoints.Length)
                        {
                            currentWaypointIndex = 2;
                            isRewind = false;

                        }

                    }

                }
            }
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
            isRewind = false;
        }
        
       
    }
    //override protected void OnRewind()
    //{
    //    isRewind = true;
    //}
}
