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

        // Keep cursor visible and free (do not hide or lock it)
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Pause()
    {
        pausedMenu.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;

        // Ensure cursor is visible and free when paused
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Quit()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}