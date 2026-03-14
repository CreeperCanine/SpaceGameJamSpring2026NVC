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
        if(tag == "ShipPart" && inventory.NumberOfParts != inventory.maxShipPart) { inventory.shipPartPickedUp(); gameObject.SetActive(false); }
        else if(tag == "Berry" && inventory.NumberOfBerry != inventory.maxBerry) { inventory.berryPickedUp(); gameObject.SetActive(false); }
        else { inventory.pickUpCollectable(); gameObject.SetActive(false); }
    }
}
