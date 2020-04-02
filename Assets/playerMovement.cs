using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public CharacterController controller;

    public Transform groundCheck;

    public LayerMask groundMask;

    public float speed = 10f;
    public float speedNorm = 10f;
    public float speedSprint = 20f;
    public float groundDistance = -.2f;
    public float gravity = -30f;
    public float jumpHeight = .5f;

    private bool isSprinting;
    private bool isGrounded;

    Vector3 velocity;

    private void Start()
    {
        speed = speedNorm;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            speed = speedSprint;
        }
        else
        {
            isSprinting = false;
            speed = speedNorm;
        }
    }
}
