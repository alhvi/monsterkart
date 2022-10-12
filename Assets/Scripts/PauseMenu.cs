using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    static bool GameIsPaused = false;
    public GameObject UI;

 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (GameIsPaused) { Continue(); }
            else { Pause(); }
        }
    }

    public void Continue()
    {
        UI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        UI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void toMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void exit()
    {
        Application.Quit();
    }
}
