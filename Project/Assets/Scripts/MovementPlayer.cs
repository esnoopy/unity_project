using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [Header("References")]
    private CharacterController controller;
    [SerializeField] private Transform camera;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float sprintTransitSpeed = 5f;
    [SerializeField] private float turningSpeed = 2f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 2f;

    private float verticalVelocity;
    private float speed;

    [Header("Mouse Look Setting")]
    [SerializeField] private float mouseSensitivity = 100f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    [Header("Input")]
    private float moveInput;
    private float turnInput;

    [Header("Animation")]
    private Animator animator;

    private void Start(){
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update(){
        InputManagement();
        MouseLook();
        Movement();
    }

    private void MouseLook(){
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; //deltaTime to be consistent, not to get different inputs depending on different frame rates
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
 
        //control rotation around x axis (Look up and down)
        xRotation -= mouseY;  //up negative down positive
 
        //we clamp the rotation so we cant Over-rotate (like in real life)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  //can look 90 degrees up and down cant look backwards
 
        //control rotation around y axis (Look left and right)
        yRotation += mouseX;
 
       //applying both rotations
       transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);  //rotation of the transform relative to the transform rotation of the parent
    }

    private void Movement(){
        GroundMovement();
        Turn();
    }

    private void GroundMovement(){
        Vector3 move = new Vector3(turnInput, 0, moveInput);
        move = transform.TransformDirection(move);  //make visible to not make it move with keys
        //move = camera.transform.TransformDirection(move);  //delete to not move with keys

//https://www.youtube.com/watch?v=5mlwvbu1fxQ
        if(move.magnitude < 0.1f){
            //Idle
            animator.SetFloat("Speed", 0f);
            animator.SetBool("isSprinting", false);
        }else if(Input.GetKey(KeyCode.LeftShift)){
            speed = Mathf.Lerp(speed, sprintSpeed, sprintTransitSpeed*Time.deltaTime);
            animator.SetFloat("Speed", 1f);
            animator.SetBool("isSprinting", true);
        }else{
            speed = Mathf.Lerp(speed, walkSpeed, sprintTransitSpeed*Time.deltaTime);
            animator.SetFloat("Speed", 0.5f);
            animator.SetBool("isSprinting", false);
        }

        move *= speed;

        move.y = VerticalForceCalculation();

        controller.Move(move*Time.deltaTime);
    }

    private void Turn(){
        if(Mathf.Abs(turnInput)>0 || Mathf.Abs(moveInput)>0){
            Vector3 currentLookDirection = camera.forward;  //make visible to not make with keys
            //Vector3 currentLookDirection = controller.velocity.normalized;  //delete to not move with keys
            currentLookDirection.y = 0;

            //currentLookDirection.Normalize(); //delete to not move with keys

            Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection);
    
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime*turningSpeed);
        }
    }

    private float VerticalForceCalculation(){
        if(controller.isGrounded){
            
            verticalVelocity = 0f;
            if(Input.GetButtonDown("Jump")){
                verticalVelocity = Mathf.Sqrt(jumpHeight*gravity*2);
            }
        }else{
            verticalVelocity -= gravity*Time.deltaTime;
        }
        return verticalVelocity;
    }

    private void InputManagement(){
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }
}
