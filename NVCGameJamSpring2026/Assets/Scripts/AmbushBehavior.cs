/*
 This is the Ambush's AI. All of its behavior can be found here
  
----------------------------------------------------------------------------------------------------------------------------
|PROGRAMMERS NOTE: If the ambusher isn't moving for some reason, try removing and then reattaching the EnemyMovement script|
----------------------------------------------------------------------------------------------------------------------------
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
    GameObject PLAYER_REFERENCE; //constant reference to the player so that enemy knows where player is
    [SerializeField]
    Transform RAY_START; //constant reference to the RAY_START object's transform component, so that the position may be called at anytime
    EnemyMovement Move; //Reference to the movement script on Ambush
    Vector3 rayStartPoint; //felt easier to put rayStartPoint as opposed to RAY_START.position everytime so I adde it here
    RaycastHit[] hitArray = new RaycastHit[9]; //array to hold the ambusher's 'eyes', felt more efficient than having 9 separate variables
    List<Transform> ObstacleList = new List<Transform>(); //array that holds observed obstacles
    Transform toMove; //variable that holds transform of given object tagged 'Obstacle'
    Vector3 desiredLocation = new Vector3 (0, 0, 0); //variable that holds the Vector3 that will dictate where ambusher moves
    public Collider attackTrig;
    Animator animControl;
    Vector3 prevLoc = new Vector3(0, 0, 0);

    [SerializeField]
    public bool flashed = false; //trigger to enter flee state ____
    [SerializeField]   //                                   \__ serialized for ease of testing
    public bool aggro = false; //trigger to enter attack state ____/
    [SerializeField]
    float posOffset; //float to offset x and z values of desired location
    // Start is called before the first frame update
    void Start()
    {
        Move = GetComponent<EnemyMovement>(); //stores reference to movement script
        animControl = GetComponent<Animator>();
        prevLoc = transform.position;
        StartCoroutine(stateControl()); //starts coroutine that is responsible for enemy behavior
    }
    private void Update()
    {
        if (!flashed)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(PLAYER_REFERENCE.transform.position.x - transform.position.x, 0, PLAYER_REFERENCE.transform.position.z - transform.position.z), transform.up); //constantly rotating x and z angle towards player
        }
        else {
            transform.rotation = Quaternion.LookRotation(new Vector3(PLAYER_REFERENCE.transform.position.x + transform.position.x, 0, PLAYER_REFERENCE.transform.position.z + transform.position.z), transform.up); //looks away from the player when flashed
        }
        rayStartPoint = RAY_START.position; //constantly getting RAY_START position

    }
    IEnumerator stateControl() 
    {
        Debug.Log("Running!");
        while (true)
        {
            while (flashed == false && aggro == false) //initiates "stalking" pattern
            {
                stalkState();
                yield return new WaitForSeconds(Vector3.Distance(transform.position, desiredLocation)/20f);//waits 2 seconds before starting next loop
                animControl.SetBool("isWalking", false);
                yield return new WaitForSeconds(.5f);
            }
            //Upon exiting the loop, this means the ambush has been either aggroed or flashed, and in both cases this coroutine will come to an end and either attack or flee state will be called
            if (flashed == true) //enters flee state
            {
                fleeState();
                yield return new WaitForSeconds(Vector3.Distance(transform.position,transform.position - new Vector3(20, 0, 20))/20f);
                flashed = false;//sets flashed to false
                animControl.SetBool("isWalking", false);
                Move.WarpToPoint(transform.position + new Vector3(-20, 0, -20));//Moves ambusher to set point far away to give the impression that it 'disappeared' to player
                yield return new WaitForSeconds(5f);//disabled ambusher for however long need be
            }
            else if (aggro == true) //enters attack state
            {
                while (flashed == false) //remains attacking until flashed
                {
                    if (Vector3.Distance(transform.position, PLAYER_REFERENCE.transform.position) > 6f) //will continue to set move point until close enough to the player
                    {
                        Move.MoveToPoint(PLAYER_REFERENCE.transform.position - new Vector3(-3, 0, -3)); //moves ambusher close to player
                        animControl.SetBool("isWalking", true);
                    }
                    else
                    {
                        animControl.SetBool("isWalking", false);
                        animControl.SetTrigger("AttackAnimTrigger");
                        yield return new WaitForSeconds(1.75f);
                        attackTrig.enabled = true;//attacks player
                        yield return new WaitForSeconds(.75f);
                        attackTrig.enabled = false;
                    }
                    yield return new WaitForSeconds(.5f); //waits fo 2 seconds
                }
                aggro = false;
            }
        }
    }
    void stalkState()
    {
            for (int i = 0; i < hitArray.Length; i++) //This for loop sends each RaycastHit inside the Array in the Physics.Raycast function, and for each obstacle it hits, saves it to the Obstacle List
            {                                         
                Physics.Raycast(rayStartPoint, transform.forward + transform.right * (-1f + (0.25f * i)), out hitArray[i], Mathf.Infinity); //Sends out Ray
                if (hitArray[i].collider != null && hitArray[i].collider.tag == "Obstacle")
                {
                    ObstacleList.Add(hitArray[i].collider.transform); //Adds object to List if it is tagged 'Obstacle'
                }
            }
            foreach (Transform t in ObstacleList) //Gets the position of each object in the List, decides which is closest to the player
            {
                if (toMove != null && Vector3.Distance(t.position, PLAYER_REFERENCE.transform.position) < Vector3.Distance(toMove.position, PLAYER_REFERENCE.transform.position) || toMove == null) //checks distance of object to player
                {
                    toMove = t;
                }
            } //the result is toMove is equal to the object closest to the player

            if (toMove != null) //If statement to prevent trying to access properties of a null
            {
                desiredLocation.x = PLAYER_REFERENCE.transform.position.x < toMove.position.x ? toMove.position.x + posOffset : toMove.position.x - posOffset; //will set desiredLocation.x to a value that will place enemy on the opposite Quadrant of object.x
                desiredLocation.z = PLAYER_REFERENCE.transform.position.z < toMove.position.z ? toMove.position.z + posOffset : toMove.position.z - posOffset;//will set desiredLocation.z to a value that will place enemy on the opposite Quadrant of object.z
                desiredLocation.y = toMove.position.y; //i don't think this is neccessary but it works with this here so I'm not touching it
                Debug.Log(desiredLocation - prevLoc);
                if(desiredLocation != prevLoc)
                {
                
                        Move.MoveToPoint(desiredLocation); //Moves enemy to Vector3 desiredLocation
                        animControl.SetBool("isWalking", true);
                }
                prevLoc = desiredLocation;

            }
            ObstacleList.Clear(); //clears list to free Memory
    }

    void fleeState() 
    {
        //Values used for code below to be changed as neccesary
        Move.SetSpeed(20f);//Makes ambusher faster so that escape is easier
        Move.MoveToPoint(transform.position - new Vector3(20, 0, 20));//moves ambusher set amount of units away from current position in attempt to get far away from playe
        animControl.SetBool("isWalking", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        //this function would check to see if ambusher gets flashed and set the flashed variable to true if so
    }
}
