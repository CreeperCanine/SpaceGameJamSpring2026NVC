/*
 This is the Ambush's AI. All of its behavior can be found here
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AmbushBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject Player_Reference; //
    [SerializeField]
    GameObject POVStart;
    EnemyMovement Move; //Reference to the movement script on Ambush
    Vector3 rayStartPoint;
    RaycastHit[] hitArray = new RaycastHit[4];
    float[] rayOffsets = {-2, -1, 0, 1, 2};
    // Start is called before the first frame update
    void Start()
    {
        Move = GetComponent<EnemyMovement>(); //stores reference to movement script
    }
    private void Update()
    {
        rayStartPoint = POVStart.transform.position;
        transform.LookAt(Player_Reference.transform.position);
        Physics.Raycast(rayStartPoint, new Vector3(transform.forward.x - 3f, transform.forward.y - 6f, transform.forward.z -3f), out hitArray[0], Mathf.Infinity);
        Physics.Raycast(rayStartPoint, new Vector3(transform.forward.x - 1.5f, transform.forward.y - 6f, transform.forward.z - 3f), out hitArray[2], Mathf.Infinity);
        Physics.Raycast(rayStartPoint, new Vector3(transform.forward.x + 0, transform.forward.y - 6f, transform.forward.z - 3f), out hitArray[2], Mathf.Infinity);
        Physics.Raycast(rayStartPoint, new Vector3(transform.forward.x + 1.5f, transform.forward.y - 6f, transform.forward.z - 3f), out hitArray[2], Mathf.Infinity);
        Physics.Raycast(rayStartPoint, new Vector3(transform.forward.x +3f, transform.forward.y - 6f, transform.forward.z - 3f), out hitArray[2], Mathf.Infinity);
        Debug.DrawRay(rayStartPoint, new Vector3(transform.forward.x - (transform.position.x - 3f), 0, transform.forward.z - transform.position.z) - rayStartPoint);
        Debug.DrawRay(rayStartPoint, new Vector3(transform.forward.x - (transform.position.x - 1.5f), 0, transform.forward.z - transform.position.z) - rayStartPoint);
        Debug.DrawRay(rayStartPoint, new Vector3(transform.forward.x - (transform.position.x + 0f), 0, transform.forward.z - transform.position.z) - rayStartPoint);
        Debug.DrawRay(rayStartPoint, new Vector3(transform.forward.x - (transform.position.x + 1.5f), 0, transform.forward.z - transform.position.z) - rayStartPoint);
        Debug.DrawRay(rayStartPoint, new Vector3(transform.forward.x -(transform.position.x + 3f), 0, transform.forward.z - transform.position.z) - rayStartPoint);
        foreach(RaycastHit hit in hitArray)
        {
            Debug.Log(hit.collider);
        }
    }
    IEnumerator stalkState() 
    {
        
        yield return null; 
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
