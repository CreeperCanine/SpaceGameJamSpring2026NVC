/*
This script is meant to hold functions that allow the enemy to move along the navmesh, and be used by
their respective behavior scripts. Like a toolbox with a bunch of tools in it
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
    }
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }
}
