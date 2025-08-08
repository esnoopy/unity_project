
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

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
    private float currentYaw = 0f;

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

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void Update(){
        InputManagement();
        MouseLook();
        Movement();
    }

    private void MouseLook()
{
    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    xRotation -= mouseY;
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    // Always apply vertical camera rotation
    camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    // Check movement input
    bool isMoving = Mathf.Abs(moveInput) > 0.1f || Mathf.Abs(turnInput) > 0.1f;

    // Only apply horizontal rotation when moving
    /*if (isMoving)
    {
        yRotation += mouseX;
        currentYaw = yRotation;
    }*/

    // Always apply horizontal rotation to player
    yRotation += mouseX;
    currentYaw = yRotation;

    // Apply rotation to player body
    transform.localRotation = Quaternion.Euler(0f, currentYaw, 0f);
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
