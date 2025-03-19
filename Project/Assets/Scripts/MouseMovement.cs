using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
 
    public float mouseSensitivity = 100f;  //change it also at the sensitivity tab 
 
    float xRotation = 0f;
    float YRotation = 0f;
 
    void Start()
    {
      //Locking the cursor to the middle of the screen and making it invisible
      Cursor.lockState = CursorLockMode.Locked;
    }
 
    void Update()  //method runs every frame
    {
       float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; //deltaTime to be consistent, not to get different inputs depending on different frame rates
       float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
 
       //control rotation around x axis (Look up and down)
       xRotation -= mouseY;  //up negative down positive
 
       //we clamp the rotation so we cant Over-rotate (like in real life)
       xRotation = Mathf.Clamp(xRotation, -90f, 90f);  //can look 90 degrees up and down cant look backwards
 
       //control rotation around y axis (Look left and right)
       YRotation += mouseX;
 
       //applying both rotations
       transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);  //rotation of the transform relative to the transform rotation of the parent
 
    }
}