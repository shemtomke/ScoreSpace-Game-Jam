using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    //play game
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    //exit game
    public void ExitGame()
    {
        Application.Quit();
    }
}
