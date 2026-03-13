using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float totalHealth;
    private float currentOxygen;
    public float totalOxygen;

    public GameObject theoryBar; // The way I understand it we are dealing with a 2D ui element here
    public GameObject theoryBar2;
    private void Start()
    {
        currentOxygen = totalOxygen;
        StartCoroutine(depleteOxygen());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Attack")
        {
            totalHealth -= 10;
            updateHealthBar();
        }
    }
    
    private void updateHealthBar()
    {
        float valueToChange = totalHealth/100;
        theoryBar.transform.localScale = new Vector3(0, valueToChange, 0);
    }

    private void updateOxyBar()
    {
        float valueToChange = totalHealth / 100;
        theoryBar.transform.localScale = new Vector3(0, valueToChange, 0);
    }

    IEnumerator depleteOxygen()
    {
        while (currentOxygen > 0)
        {
            currentOxygen -= 1;
            Debug.Log(currentOxygen/totalOxygen);
            updateOxyBar();
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Out of O2");

        while(currentOxygen <= 0)
        {
            totalHealth -= 5;
            Debug.Log(totalHealth);
            yield return new WaitForSeconds(1.5f);
            updateHealthBar();
        }
        Debug.Log("Out of health");
        
    }

}
