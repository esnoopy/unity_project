using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEmptyCSharpScript : MonoBehaviour{

    public GameObject camera1;
    public GameObject camera2;
    public int Manager;

    public void Update(){
        if (Input.GetKeyDown(KeyCode.C))
        {
            ManageCamera();
        }
    }

    public void ManageCamera(){
        if(Manager == 0){
            Cam2();
            Manager = 1;
        }else{
            Cam1();
            Manager = 0;
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
