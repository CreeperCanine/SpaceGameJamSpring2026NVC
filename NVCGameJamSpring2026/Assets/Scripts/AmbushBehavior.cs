/*
 This is the Ambush's AI. All of its behavior can be found here
 */
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AmbushBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject PLAYER_REFERENCE; //
    [SerializeField]
    Transform RAY_START;
    EnemyMovement Move; //Reference to the movement script on Ambush
    Vector3 rayStartPoint;
    RaycastHit[] hitArray = new RaycastHit[9];
    List<Transform> ObstacleList = new List<Transform>();
    Transform temp;
    Transform toMove;
    Vector3 desiredLocation = new Vector3 (0, 0, 0);
    bool flashed = false;
    bool aggro = false;
    public float posOffset;
    // Start is called before the first frame update
    void Start()
    {
        Move = GetComponent<EnemyMovement>(); //stores reference to movement script
        StartCoroutine(stalkState());
    }
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(PLAYER_REFERENCE.transform.position.x - transform.position.x, 0, PLAYER_REFERENCE.transform.position.z - transform.position.z), transform.up);
        rayStartPoint = RAY_START.position;
    }
    IEnumerator stalkState() 
    {
        Debug.Log("Entered stalk state");
        flashed = false;
        while (flashed == false || aggro == false)
        {
            Debug.Log("Entered while");
            for (int i = 0; i < hitArray.Length; i++)
            {
                Physics.Raycast(rayStartPoint, transform.forward + transform.right * (-1f + (0.25f * i)), out hitArray[i], Mathf.Infinity);
                Debug.DrawRay(rayStartPoint, transform.forward + transform.right * (-1f + (0.25f * i)));
                if (hitArray[i].collider != null && hitArray[i].collider.tag == "Obstacle")
                {
                    ObstacleList.Add(hitArray[i].collider.transform);
                }
                foreach (Transform t in ObstacleList)
                {
                    if (toMove != null && Vector3.Distance(t.position, PLAYER_REFERENCE.transform.position) < Vector3.Distance(toMove.position, PLAYER_REFERENCE.transform.position) || toMove == null)
                    {
                        toMove = t;
                    }
                }

                if (toMove != null && ObstacleList.Count > 3)
                {
                    desiredLocation.x = PLAYER_REFERENCE.transform.position.x < toMove.position.x ? toMove.position.x + posOffset : toMove.position.x - posOffset;
                    desiredLocation.z = PLAYER_REFERENCE.transform.position.z < toMove.position.z ? toMove.position.z + posOffset : toMove.position.z - posOffset;
                    desiredLocation.y = toMove.position.y;
                    Move.MoveToPoint(desiredLocation);
                }
            }
            ObstacleList.Clear();
            yield return null;
        }
        if(flashed == true)
        {
            StartCoroutine(fleeState());
        }
        else if(aggro == true)
        {
            StartCoroutine(attackState());
        }
    }
    IEnumerator attackState() 
    {
        yield return null; 
    }
    IEnumerator fleeState() 
    {
        yield return null; 
    }
}
