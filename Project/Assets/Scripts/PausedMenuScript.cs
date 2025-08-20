// PausedMenuScript.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedMenuScript : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pausedMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pausedMenu.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    void Pause()
    {
        pausedMenu.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void Quit()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}