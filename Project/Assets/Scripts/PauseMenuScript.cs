using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour{

    public static bool GamePaused = false;
    public GameObject pausemenu;

    void Update(){

        if(Input.GetKeyDown(KeyCode.P)){
            //https://discussions.unity.com/t/unlock-cursor/167385/4
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if(GamePaused){
                Resume();
            }else{
                Pause();
            }
        }
    }

    public void Resume(){
        pausemenu.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Pause(){
        pausemenu.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void QuitMenu(){
        Debug.Log("QUIT");
        Application.Quit();
    }
    
}
