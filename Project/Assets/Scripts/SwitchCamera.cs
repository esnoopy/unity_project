using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour{
    
    public GameObject camera1;
    public GameObject camera2;
    public int managecam;

    public void Update(){

        if(Input.GetKeyDown(KeyCode.C)) {
            ManageCamera();
        }
    }
    public void ManageCamera(){

        if(managecam == 0){
            Cam2();
            managecam = 1;
        }else{
            Cam1();
            managecam = 0;
        }
    }
    void Cam1(){

        camera1.SetActive(true);
        camera2.SetActive(false);
    }

    void Cam2(){

        camera1.SetActive(false);
        camera2.SetActive(true);
    }
}
