using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame() { SceneManager.LoadScene(1); Debug.Log("Start"); } //
    public void Quit() { Application.Quit(); Debug.Log("Quit"); }
}