using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable
{
    GameObject player;
    
    public void Interact()
    {
        player = GameObject.FindWithTag("Player"); // Set the Player gameObject tag in the hierarchy to "Player" to get this to work.
        PlayerInventory inventory = player.GetComponent<PlayerInventory>();
        inventory.CollectablePickedUp();
        gameObject.SetActive(false);
    }
}
