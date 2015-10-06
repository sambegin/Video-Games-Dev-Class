using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{

    private float speed = 20.0f;
    private float jumpHeight = 15.0f;
    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 50.0f;
    public int rotateSpeed = 1;

    void Start()
    {
       
    }

    public void Update()
    {
        handleRotation();
    }

    private void handleRotation()
    {
        //float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        Debug.Log(Input.mouseScrollDelta.x);
        float horizontal = Input.mouseScrollDelta.x * rotateSpeed;
        transform.Rotate(0, horizontal, 0);
        //if (mouseOnRightEdge())
        //{
        //    Vector3 rotateRight = new Vector3(0, 1, 0);
        //    transform.Rotate(rotateRight * Time.deltaTime * rotateSpeed, Space.Self);
        //}
        //if (mouseOnLeftEdge())
        //{
        //    Vector3 rotateLeft = new Vector3(0, -1, 0);
        //    transform.Rotate(rotateLeft * Time.deltaTime * rotateSpeed, Space.Self);
        //}
    }

    //private static bool mouseOnLeftEdge()
    //{
    //    return Input.mousePosition.x <= Screen.width * 0.05;
    //}

    //private static bool mouseOnRightEdge()
    //{
    //    return Input.mousePosition.x >= Screen.width * 0.95;
    //}

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
