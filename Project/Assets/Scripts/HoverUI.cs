using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverUI : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject bookBtn;
    public GameObject book;
    public GameObject closeBtn;
    //public MonoBehaviour mouseLookScript;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
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
        /*if (mouseLookScript != null) // Enable mouse look
        {
            mouseLookScript.enabled = true;
        }
        bookBtn.SetActive(true);*/
        //Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GamePaused = false;
    }

    void Pause()
    {
        bookBtn.SetActive(true);
        /*if (mouseLookScript != null) // Disable mouse look
        {
            mouseLookScript.enabled = false;
        }*/
        //Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None; // Unlock cursor
        Cursor.visible = true;
        GamePaused = true;
    }

    public void bookOpen()
    {
        book.SetActive(true);
    } 
    
    public void bookClose()
    {
        book.SetActive(false);
    } 
    
}
