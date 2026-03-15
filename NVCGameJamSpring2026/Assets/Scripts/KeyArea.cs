using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


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
                SceneManager.LoadScene("2");
            }
        else
            {
                SceneManager.LoadScene("2");
            }   
        }
    }
}
