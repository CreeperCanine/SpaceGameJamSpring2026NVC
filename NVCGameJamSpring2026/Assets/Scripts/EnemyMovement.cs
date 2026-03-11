/*
This script is meant to hold functions that allow the enemy to move along the navmesh, and be used by
their respective behavior scripts
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent; //reference to the gameobject's 
    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = 20.0f;
    }
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }

    public void WarpToPoint(Vector3 point)
    {
        agent.Warp(point);
    }

    public void SetSpeed(float speed)
    {
        agent.speed = speed;
    }

}
