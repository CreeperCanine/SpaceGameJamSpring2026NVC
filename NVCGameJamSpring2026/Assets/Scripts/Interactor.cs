using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    // Any item that is to be interacted with must inherit from the IInteractable class and implement the Interact() function.
    // Place the behavior to be activated in the Interact() function.
    public void Interact();
}
public class Interactor : MonoBehaviour // Attach this script to the Main Camera.
{
    public Transform InteractorSource;
    public float InteractRange;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}
