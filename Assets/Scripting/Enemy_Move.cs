using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy_Move : MonoBehaviour
{
    AIPath forwardMovement;
    public List<GameObject> wayPointsinRange;
    AIDestinationSetter destinationSetter;
    public bool resumeWaypoints;
    public float rangeToFindWaypoint = 20f;
    // Start is called before the first frame update
    void Start()
    {
        forwardMovement = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        //GetWayPoints();
        destinationSetter.target = wayPointsinRange[0].transform;
    }

    // Update is called once per frame
    void Update()
    {
        System.Random rnd = new System.Random();
        if (resumeWaypoints)
        {
            //GetWayPoints();
            int randomNumber = rnd.Next(0, wayPointsinRange.Count - 1);
            resumeWaypoints = false;
            destinationSetter.target = wayPointsinRange[randomNumber].transform;
        }
        else if(forwardMovement.reachedEndOfPath)
        {
            int randomNumber = rnd.Next(0, wayPointsinRange.Count - 1);
            destinationSetter.target = wayPointsinRange[randomNumber].transform;
        }
    }
}
