using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
}
public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward); //If i could make a suggestion, it would be to implement a collider instead of a raycast or multiple raycasts to detect collectables
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))        //That way it's easier to pick up the objects
            {                                                                     //-David
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}
