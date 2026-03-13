// Matthew Reyna
// Goddard's Gambit
// Day and Night Cycle

// NOTE: Attach this script to a directional light. In the inspector drag Light (located under Transform in the inspector) into the empty "Sun" field

using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    
    public Light sun;

    // These variables are set to 2 parts daytime to 1 part nightime in the name of verisimilitude. Even if on an alien planet the day to night ratio may be wildly different to our own.
    // These variables should be messed with by the level designers in the inspector to get the right amount.
    // rotation speed should be lower during the daytime and higher at night, unless you want shorter days and longer nights, then swap.
    // The current values are set to go very fast to demenstrate that it works

    public float dayRotationSpeed = 25f;
    public float nightRotationSpeed = 75f;

    void Update()
    {
        // Check if the sun is above the horizon
        bool isDay = sun.transform.forward.y < 0;

        // Ternary to check which rotation speed to use based off of the horizon
        float currentSpeed = isDay ? dayRotationSpeed : nightRotationSpeed;

        // Rotating the directional light "sun"
        sun.transform.Rotate(Vector3.right, currentSpeed * Time.deltaTime);
    }
}