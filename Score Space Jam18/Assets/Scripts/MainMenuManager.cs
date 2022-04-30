using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    //Load Intro
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    //Mute Game

    //exit game
    public void ExitGame()
    {
        Application.Quit();
    }
}
