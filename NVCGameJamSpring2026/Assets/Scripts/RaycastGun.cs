using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RaycastGun : MonoBehaviour
{
    public Camera playerCamera;
    public Transform laserOrigin;
    public float gunRange = 50f;
    public float fireRate = 0.2f;
    public float laserDuration = 0.05f;
    GameObject enemy;

    LineRenderer laserLine;
    float fireTimer;

    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && fireTimer > fireRate)
        {
            fireTimer = 0;
            laserLine.SetPosition(0, laserOrigin.position);
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, gunRange))
            {
                laserLine.SetPosition(1, hit.point);
                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<AmbushBehavior>().flashed = true; //this if statement is going to check to see if it hits the enemy and then flash it
                }

                //enemy = GameObject.FindWithTag("Enemy"); just gonna run code on enemy no matter what it actually hits
                //Making adjustments to code
                
                /*
                AmbushBehavior flashEm = enemy.GetComponent<AmbushBehavior>();
                flashEm.flashed = true;
                */
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (playerCamera.transform.forward * gunRange));
            }
            StartCoroutine(ShootLaser());
        }
    }

    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}