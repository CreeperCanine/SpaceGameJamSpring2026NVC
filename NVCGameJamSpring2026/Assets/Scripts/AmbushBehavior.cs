/*
 This is the Ambush's AI. All of its behavior can be found here
 */
using System.Collections;
using System.Collections.Generic;   
using UnityEngine;

public class AmbushBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject Player_Reference; //
    Vector3 playerLocation; //stores players location as a vector3
    EnemyMovement Move; //Reference to the movement script on Ambush
    // Start is called before the first frame update
    void Start()
    {
        Move = GetComponent<EnemyMovement>(); //stores reference to movement script
    }
    private void Update()
    {
        transform.LookAt(Player_Reference.transform.position);
        RaycastHit hit;
        if(Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f), transform.forward, out hit, Mathf.Infinity)) {
            Debug.Log(hit.collider) ;
        }
        Debug.DrawLine(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1f), transform.forward);
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
