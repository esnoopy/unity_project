using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject optionsPanel;
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

    public void OpenInstructions()
    {
            instructionsPanel.SetActive(true); // Show instructions panel

            mainMenuUI.SetActive(false); // Hide main menu
    }

    // Open Options panel
    public void OpenOptions()
    {
            optionsPanel.SetActive(true); // Show options panel

            mainMenuUI.SetActive(false); // Hide main menu
    }

    // Go back to Main Menu from Instructions or Options
    public void BackToMainMenu()
    {
            mainMenuUI.SetActive(true); // Show main menu

            instructionsPanel.SetActive(false); // Hide instructions panel
        
            optionsPanel.SetActive(false); // Hide options panel
    }
}
