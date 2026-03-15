using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManagement namespace

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private float delayBeforeLoad = 12f; // Set your time delay in seconds in the Inspector
    [SerializeField] private string sceneToLoadName = "MainMenu"; // Set the name of the target scene

    // Call this function when the timer should start (e.g., in Start or after an event)
    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay(delayBeforeLoad));
    }

    private IEnumerator LoadSceneAfterDelay(float delay)
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(delay);

        // Load the target scene
        SceneManager.LoadScene(sceneToLoadName);
    }
}
