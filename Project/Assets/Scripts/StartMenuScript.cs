using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour{
    
    public void PlayGame(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void QuitGame(){

        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
