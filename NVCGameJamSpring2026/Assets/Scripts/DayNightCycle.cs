using UnityEngine;
using System;

public class DayNightCycle : MonoBehaviour
{
    public Light sun;

    public float dayRotationSpeed = 25f;
    public float nightRotationSpeed = 75f;

    public static event Action OnSunrise;
    public static event Action OnSunset;

    private bool wasDay;

    public AudioClip dayAmbience;
    public AudioClip nightAmbience;
    public AudioClip windHowl;

    void Start()
    {
        wasDay = sun.transform.forward.y < 0;
    }

    void Update()
    {
        bool isDay = sun.transform.forward.y < 0;

        float currentSpeed = isDay ? dayRotationSpeed : nightRotationSpeed;
        sun.transform.Rotate(Vector3.right, currentSpeed * Time.deltaTime);

        if (isDay && !wasDay)
        {
            Debug.Log("Sunrise");
            OnSunrise?.Invoke();
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlayMusic(dayAmbience);
        }

        if (!isDay && wasDay)
        {
            Debug.Log("Sunset");
            OnSunset?.Invoke();
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.PlayMusic(nightAmbience);
        }

        wasDay = isDay;
    }
}