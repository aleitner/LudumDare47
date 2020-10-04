using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform FLWheel;
    public Transform FRWheel;

    public CharacterController controller;

    public float rotationSpeed = 50f;
    public float speed = 12f; // Turn Speed
    public float gravity = -9.81f;

    
    public Transform groundCheck; // Reference to Object
    public float groundDistance = 0.4f; // Radius of sphere groundCheck object
    public LayerMask groundMask; // Control what objects the sphere should check for

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        // reset car when looping or when falling
        if (transform.position.z > 50 || transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 25.49f, 9f);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.1f;
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = transform.forward * z;

        // Turning
        if (move != Vector3.zero)
        {
            float direction = (Vector3.Dot(transform.forward, move) > 0) ? 1 : -1;
            Vector3 rotation = Vector3.up * x * rotationSpeed * Time.deltaTime * direction;
            transform.Rotate(rotation);
            
        }

        // Move car across x axis        
        controller.Move(move * speed * Time.deltaTime);
    }
}
