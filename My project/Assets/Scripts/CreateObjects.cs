using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SpawnPoint
{
    public Transform position;

    [Range(0, 100)]
    public float spawnChance = 50f;
}

public class CreateObjects : MonoBehaviour
{
    public GameObject sunrisePrefab;
    public GameObject sunsetPrefab;

    public SpawnPoint[] spawnPoints;

    public bool clearObjectsEachCycle = true;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    void OnEnable()
    {
        DayNightCycle.OnSunrise += SpawnSunriseObjects;
        DayNightCycle.OnSunset += SpawnSunsetObjects;
    }

    void OnDisable()
    {
        DayNightCycle.OnSunrise -= SpawnSunriseObjects;
        DayNightCycle.OnSunset -= SpawnSunsetObjects;
    }

    void SpawnSunriseObjects()
    {
        SpawnObjects(sunrisePrefab);
    }

    void SpawnSunsetObjects()
    {
        SpawnObjects(sunsetPrefab);
    }

    void SpawnObjects(GameObject prefabToSpawn)
    {
        if (clearObjectsEachCycle)
        {
            ClearObjects();
        }

        foreach (SpawnPoint point in spawnPoints)
        {
            float roll = Random.Range(0f, 100f);

            if (roll <= point.spawnChance)
            {
                GameObject newObject = Instantiate(
                    prefabToSpawn,
                    point.position.position,
                    prefabToSpawn.transform.rotation
                );

                spawnedObjects.Add(newObject);
            }
        }
    }

    void ClearObjects()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        spawnedObjects.Clear();
    }
}