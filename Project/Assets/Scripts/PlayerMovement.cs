using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;  //dragged character controller of player inside the controller reference of the player script
 
    public float speed = 12f;
    public float gravity = -9.81f * 2;  //resembles real life gravity
    public float jumpHeight = 3f;
 
    public Transform groundCheck;  
    //check if we are standing on the ground, so player jumps only when standing on ground
    //Created object on player named ground check and dragged it inside the reference groundcheck of the script
    public float groundDistance = 0.4f;  //distance between the ground check and the ground
    //ground check needs to be at the bottom of the player
    public LayerMask groundMask;  //detect layer when doing ground check
 
    Vector3 velocity;  //velocity of the fall
 
    bool isGrounded;  //if grounded or not
 
    // Update is called once per frame
    void Update()
    {
        //checking if we hit the ground to reset our falling velocity, otherwise we will fall faster the next time
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);  //check sphere sends some kind of sphere from ground check and check if there is something inside the sphere
        //check for a ground at the beginning so assign one, in terrain inspector layers add layer (name Ground) and change the layer to ground from default and in script change the groundmark refernce to ground
 
        if (isGrounded && velocity.y < 0)  //resetting the velocity
        {
            velocity.y = -2f;
        }
 
        float x = Input.GetAxis("Horizontal");  //checks if it is -1 or 1
        float z = Input.GetAxis("Vertical");  //checks if it is -1 or 1 (forward or backward)
 
        //right is the red Axis, foward is the blue axis
        Vector3 move = transform.right * x + transform.forward * z;  //checks direction of x axis by multiple with the red axis and direction of z axis multiply by blue axis
 
        controller.Move(move * speed * Time.deltaTime);  //move method supplies the movement of a game object with an attached character controller component

//https://www.reddit.com/r/Unity3D/comments/ouu56x/no_diagonal_movment_input/?rdt=41043
        if(Input.GetKey("up") && Input.GetKey("right")){
            Vector3 moveDiagonal = Vector3.Normalize(new Vector3(x, 0f, z));
        }

        //check if the player is on the ground so he can jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //the equation for jumping
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
 
        velocity.y += gravity * Time.deltaTime;
 
        controller.Move(velocity * Time.deltaTime);
    }
}