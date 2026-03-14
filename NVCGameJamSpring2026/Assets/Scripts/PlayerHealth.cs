using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    public float totalHealth;
    private float currentOxygen;
    public float totalOxygen;
    public float currentHealth;
    public GameObject[] oxyBar = new GameObject[6];
    public Image healthbar;
    GameObject gameOver;
    FirstPersonController controller;
    GameObject ui;
    private void Start()
    {
        currentOxygen = totalOxygen;
        currentHealth = totalHealth;
        controller = GetComponent<FirstPersonController>();
        gameOver = GameObject.FindWithTag("GameOver");
        gameOver.SetActive(false);
        ui = GameObject.FindWithTag("O2Health");
        StartCoroutine(depleteOxygen());
    }
    
    public void updateHealthBar(float damage)
    {
        currentHealth -= damage;
        healthbar.fillAmount = currentHealth / totalHealth;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void updateOxyBar()
    {
        if ((currentOxygen / totalOxygen) >= .81) 
        {
            oxyBar[5].SetActive(true);
        }
        else if((currentOxygen / totalOxygen) >= .61)
        {
            oxyBar[5].SetActive(false);
            oxyBar[4].SetActive(true);
        }
        else if ((currentOxygen / totalOxygen) >= .41)
        {
            oxyBar[4].SetActive(false);
            oxyBar[3].SetActive(true);
        }
        else if ((currentOxygen / totalOxygen) >= .21)
        {
            oxyBar[3].SetActive(false);
            oxyBar[2].SetActive(true);
        }
        else if ((currentOxygen / totalOxygen) >= .1)
        {
            oxyBar[2].SetActive(false);
            oxyBar[1].SetActive(true);
        }
        else if ((currentOxygen / totalOxygen) <= 0)
        {
            oxyBar[1].SetActive(false);
            oxyBar[0].SetActive(true);
        }
    }

    void Die()
    {
        controller.enabled = false;
        ui.SetActive(false);
        ui = null;
        gameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
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
            updateHealthBar(5);
            yield return new WaitForSeconds(1.5f);
            
        }
        Debug.Log("Out of health");
        
    }
}
