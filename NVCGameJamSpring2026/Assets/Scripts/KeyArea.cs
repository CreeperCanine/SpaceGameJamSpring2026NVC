using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class KeyArea : MonoBehaviour
{
    GameObject player;
    private float delayBeforeLoad = 10f; // Set your time delay in seconds in the Inspector
    private string sceneToLoadName; // Set the name of the target scene

    public void OnTriggerEnter(Collider other)
    {
        player = GameObject.FindWithTag("Player");
        PlayerInventory inventory = player.GetComponent<PlayerInventory>();
        
        if (inventory.NumberOfCollectables == 3) 
        {
            if (inventory.NumberOfBerry < 6)
            {
                StartCoroutine(LoadBadEndAfterDelay(10f));
            }
        else
            {
                StartCoroutine(LoadGoodEndAfterDelay(10f));
            }   
        }
    }

    private IEnumerator LoadGoodEndAfterDelay(float delay)
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(delayBeforeLoad);

        // Load the target scene
        SceneManager.LoadScene("GoodEnding");
    }

    private IEnumerator LoadBadEndAfterDelay(float delay)
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(delayBeforeLoad);

        // Load the target scene
        SceneManager.LoadScene("BadEnding");
    }
}
