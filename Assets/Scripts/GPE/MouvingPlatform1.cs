using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvingPlatform1 : RewindableObject
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;
    [SerializeField] private float speed = 1f;
    private float currentTimerValue;
    [SerializeField] private float watingTimeAtWaypoints;
    public bool isOn;
    
    public bool isRewind;


    void Update()
    {
        // Seulement si la plateforme est activée
        if (isOn)
        {
            // Attente quand on touche un waypoint
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f)
            {
                // Timer
                currentTimerValue -= Time.deltaTime;

                // Sélectionner le prochain waypoint
                if (currentTimerValue <= 0)
                {

                    currentWaypointIndex = currentWaypointIndex + 1;

                    // Reset le timer
                    currentTimerValue = watingTimeAtWaypoints;
                    if (currentWaypointIndex >= waypoints.Length)
                    {
                        currentWaypointIndex = 0;
                    }

                }

            }

            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
        }
    }
    override protected void DuringRewind()
    {
        // Activé uniquement à la frame où le Rewind commence
        if (!isRewind)
        {
            isRewind = true;
            speed = - Mathf.Abs(speed);

            // Réarange l'ordre des waypoint pour que ça aille dans l'autre sens
            GameObject[] tempWaypoints = waypoints;
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = tempWaypoints[waypoints.Length - i];
            }
        }
    }

    // Reset la vitesse à sa valeur positive & cie
    protected override void EndRewind()
    {
        speed = Mathf.Abs(speed);
        isRewind = false;

        // Re-réarange l'ordre des waypoint pour que ça aille dans le vrai bon sens (c'est la même méthode qu'au dessus)
        GameObject[] tempWaypoints = waypoints;
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = tempWaypoints[waypoints.Length - i];
        }
    }
}
