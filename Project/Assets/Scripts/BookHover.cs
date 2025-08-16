using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverUI : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject bookBtn;
    public GameObject book;
    public GameObject xBook;
    //public MonoBehaviour mouseLookScript;
    public GameObject map;

    public GameObject inventory;

    public GameObject shop;

    public GameObject help;

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
        help.SetActive(false);
        shop.SetActive(false);
        inventory.SetActive(false);
        map.SetActive(false);

        book.SetActive(true);
        //xBook.SetActive(true);
    }

    public void CloseBook()
    {
        Debug.Log("Book Close Called");
        book.SetActive(false);
        //xBook.SetActive(false);
    }

    public void mapOpen()
    {
        help.SetActive(false);
        shop.SetActive(false);
        inventory.SetActive(false);
        book.SetActive(false);

        map.SetActive(true);
    }

    public void mapClose()
    {
        map.SetActive(false);
    }

    public void InventoryOpen()
    {
        help.SetActive(false);
        shop.SetActive(false);
        map.SetActive(false);
        book.SetActive(false);

        inventory.SetActive(true);
    }

    public void InventoryClose()
    {
        inventory.SetActive(false);
    }

    public void ShopOpen()
    {
        help.SetActive(false);
        map.SetActive(false);
        inventory.SetActive(false);
        book.SetActive(false);

        shop.SetActive(true);
    }

    public void ShopClose()
    {
        shop.SetActive(false);
    }

    public void HelpOpen()
    {
        map.SetActive(false);
        shop.SetActive(false);
        inventory.SetActive(false);
        book.SetActive(false);

        help.SetActive(true);
    }

    public void HelpClose()
    {
        help.SetActive(false);
    }

}