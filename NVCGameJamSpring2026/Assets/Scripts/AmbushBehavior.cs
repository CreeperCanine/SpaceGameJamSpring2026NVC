/*
 This is the Ambush's AI. All of its behavior can be found here
 */
using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        Move = GetComponent<EnemyMovement>(); //stores reference to movement script
    }
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(PLAYER_REFERENCE.transform.position.x - transform.position.x, 0, PLAYER_REFERENCE.transform.position.z - transform.position.z), transform.up);
        rayStartPoint  = RAY_START.position;
        for(int i = 0; i < hitArray.Length; i++)
        {
            Physics.Raycast(rayStartPoint, transform.forward + transform.right * (-1f + (0.25f * i)), out hitArray[i], Mathf.Infinity);
            Debug.DrawRay(rayStartPoint, transform.forward + transform.right * (-1f + (0.25f * i)));
            if (hitArray[i].collider != null)
            {
                Debug.Log("Raycast" + i + "\nHit: " + hitArray[i].collider);
                Debug.Log("Tag of Object: " + hitArray[i].collider.tag);
            }
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
