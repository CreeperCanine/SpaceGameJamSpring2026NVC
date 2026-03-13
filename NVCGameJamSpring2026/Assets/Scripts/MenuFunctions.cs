using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuFunctions : MonoBehaviour
{
    public Canvas menu;
    public Scene gameScene;
    public Slider volSlide;
    // Start is called before the first frame update
    public void StartGame() { SceneManager.LoadScene(1); } //
    public void Quit() { Application.Quit(); }
}