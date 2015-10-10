using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{

    private float speed = 20.0f;
    private float jumpHeight = 15.0f;
    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 50.0f;

    void Start()
    {

    }

    private void handleDirection()
    {
        Camera playerCamera = GetComponentInChildren<Camera>();
        Vector3 cameraPosition = playerCamera.transform.position;

        transform.LookAt(cameraPosition);
        Vector3 forwardDirection = transform.position - cameraPosition;
        transform.rotation = Quaternion.LookRotation(forwardDirection);
    }

    void FixedUpdate()
    {
        handleDirection();
        handleJump();

    }

    private void handleJump()
    {
        CharacterController controller = GetComponent<CharacterController>();

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpHeight;
            }


        }
        else
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.x *= speed;
            moveDirection.z *= speed;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
