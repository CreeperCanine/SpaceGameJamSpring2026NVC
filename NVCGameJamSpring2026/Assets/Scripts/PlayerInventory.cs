using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfCollectables { get; private set; }

    public void CollectablePickedUp()
    {
        NumberOfCollectables++;
        Debug.Log(NumberOfCollectables);
    }
}
