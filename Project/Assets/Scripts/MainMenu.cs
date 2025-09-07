using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI; 
    private void Start()
    {
        // Show menu at start
        if (mainMenuUI != null)
            mainMenuUI.SetActive(true);
    }

    public void PlayGame()
    {
        if (mainMenuUI != null)
            mainMenuUI.SetActive(false); // hide menu
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
