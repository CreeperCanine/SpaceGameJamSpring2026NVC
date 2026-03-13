using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeyArea : MonoBehaviour
{
    public int partCount = 0;
    GameObject player;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Key"))
        {
            partCount++;
            Debug.Log(partCount);
            other.gameObject.transform.position = new Vector3(0, -100, 0);
            other.gameObject.GetComponent<Rigidbody>().useGravity = false; 
        }

        player = GameObject.FindWithTag("Player");
        PlayerInventory inventory = player.GetComponent<PlayerInventory>();
        
        if (partCount == 3) 
        {
            if (inventory.NumberOfCollectables <= 6)
            {
                // Script for good ending
            }
        else
            {
                // Script for bad ending
            }   
        }
        
    }
}
