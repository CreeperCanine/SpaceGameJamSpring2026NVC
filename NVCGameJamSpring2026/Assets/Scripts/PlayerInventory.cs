using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfBerry = 0;
    public int NumberOfCollectables { get; private set; }
    public int maxBerry = 6;
    public int maxCollectables = 3;
    [SerializeField] private TextMeshProUGUI berryText;
    [SerializeField] private TextMeshProUGUI collectableText;

    private void Start()
    {
        berryText.text = $"Berries\n{NumberOfBerry}/{maxBerry}";
        collectableText.text = $"Collectables\n{NumberOfCollectables}/{maxCollectables}";
    }
    public void berryPickedUp()
    {
        NumberOfBerry++;
        berryText.text = $"Berries\n{NumberOfBerry}/{maxBerry}";
        Debug.Log(NumberOfBerry);
    }

    public void pickUpCollectable() //tbh i'm not sure if collectable is a general term of if theres some other interactable in the game but the option to pick one up is here in case we need it
    {
        NumberOfCollectables++;
        collectableText.text = $"Collectables\n{NumberOfCollectables}/{maxCollectables}";
    }
}
