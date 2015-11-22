using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{

    private LevelManager levelManager;
    private float speed = 20.0f;
    private float jumpHeight = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 50.0f;
    private float inAirDrift = 60.0f;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void FixedUpdate()
    {
        handleDirection();
        handleJump();
        handleDead();
    }

    private void handleDead()
    {
        if(transform.position.y < 0)
        {
            levelManager.playerIsDead();
        }
    }

    private void handleDirection()
    {
        Camera playerCamera = GetComponentInChildren<Camera>();
        Vector3 cameraPosition = playerCamera.transform.position;

        transform.LookAt(cameraPosition);
        Vector3 forwardDirection = transform.position - cameraPosition;
        transform.rotation = Quaternion.LookRotation(forwardDirection);
    }

    private void handleJump()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = transform.forward * speed * Input.GetAxis("Vertical") + transform.right * speed * Input.GetAxis("Horizontal");
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpHeight;
            }
        }
        else
        {
            moveDirection += transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * inAirDrift, 0, Input.GetAxis("Vertical") * Time.deltaTime * inAirDrift));
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
