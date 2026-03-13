using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().currentHealth -= 10;
            other.gameObject.GetComponent<PlayerHealth>().updateHealthBar(10);
        }
    }
}
