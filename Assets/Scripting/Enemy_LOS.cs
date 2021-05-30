using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_LOS : MonoBehaviour
{
    public static Enemy_LOS main;
	private void Awake() { main = this; }

	GameObject player;
    public float viewRadius = 10;
    public float viewAngle = 45;
    public LayerMask obstacleMask;
    private bool isAlert;
    private float originalSpeed;
    AIDestinationSetter destinationSetter;


    public bool isDetectingPlayer;
    Enemy_Move forwardMovement;
    AIPath pathing;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        forwardMovement = GetComponent<Enemy_Move>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        pathing = GetComponent<AIPath>();
        originalSpeed = pathing.maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isDetectingPlayer = CheckLineofSight();
        if(isDetectingPlayer)
        {
            pathing.maxSpeed = 1;
            destinationSetter.target = player.transform;
            isAlert = true;
        }
        if (!isDetectingPlayer && isAlert)
        {
            pathing.maxSpeed = originalSpeed;
            forwardMovement.resumeWaypoints = true;
            isAlert = false;
        }
    }

    private bool CheckLineofSight()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 dirToPlayer = (playerPos - transform.position).normalized;
        float distance = Vector3.Distance(playerPos, transform.position);
        float angleToPlayer = Vector3.Angle(transform.forward.normalized, dirToPlayer);
        if (!Physics.Raycast(transform.position, dirToPlayer, distance, obstacleMask) && angleToPlayer < viewAngle && distance < viewRadius)
        {
            return true;
        }
        return false;
    }

}
