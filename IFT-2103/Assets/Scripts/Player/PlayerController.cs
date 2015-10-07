using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{

    private float speed = 20.0f;
    private float jumpHeight = 15.0f;
    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 50.0f;
    private int rotateSpeed;

    void Start()
    {
        this.rotateSpeed = 200;
    }

    public void Update()
    {
        handleRotation();
    }

    private void handleRotation()
    {
        if (mouseOnRightEdge())
        {
            Vector3 rotateRight = new Vector3(0, 1, 0);
            transform.Rotate(rotateRight * Time.deltaTime * rotateSpeed, Space.Self);
        }
        if (mouseOnLeftEdge())
        {
            Vector3 rotateLeft = new Vector3(0, -1, 0);
            transform.Rotate(rotateLeft * Time.deltaTime * rotateSpeed, Space.Self);
        }
    }


    private static bool mouseOnLeftEdge()
    {
        return Input.mousePosition.x < Screen.width * 0.10 & Input.mousePosition.x > -1;
    }

    private static bool mouseOnRightEdge()
    {
        return Input.mousePosition.x > Screen.width * 0.90 & Input.mousePosition.x < Screen.width;
    }

    void FixedUpdate()
    {
        CharacterController controller = GetComponent<CharacterController>();

        if (controller.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump")) {
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
