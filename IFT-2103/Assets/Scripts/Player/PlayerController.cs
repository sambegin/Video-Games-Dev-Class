using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{

    private float speed = 20.0f;
    private float jumpHeight = 15.0f;
    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 50.0f;
   //private int rotateSpeed = 200;

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
        //Debug.Log(horizontal);
        //float horizontal = Input.mouseScrollDelta.x * rotateSpeed;
        //transform.Rotate(0, horizontal, 0);

        //if (mouseOnRightEdge())
        //{
        //    //double xStart = Screen.width * 0.65;
        //    //double variation = Input.mousePosition.x - xStart;
        //    //double rotateSpeedInDouble = Math.Pow(variation, 1.1);
        //    //int rotateSpeed = Convert.ToInt32(rotateSpeedInDouble);
        //    //rotateSpeed -= 200;
        //    int rotateSpeed = 200;

        //    Vector3 rotateRight = new Vector3(0, 1, 0);
        //    transform.Rotate(rotateRight * Time.deltaTime * rotateSpeed, Space.Self);
        //}
        //if (mouseOnLeftEdge())
        //{

        //    int rotateSpeed = 200;



        //    Vector3 rotateLeft = new Vector3(0, -1, 0);
        //    transform.Rotate(rotateLeft * Time.deltaTime * rotateSpeed, Space.Self);
        //}


        //transform.LookAt(camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane)));
        GameManager manger = FindObjectOfType<GameManager>();
        Camera camera = manger.getCamera();
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        RaycastHit hit = new RaycastHit();
        Vector3 lookTarget = new Vector3();
        if (Physics.Raycast(ray, out hit))
        {
            lookTarget = hit.point;
            Vector3 lookDelta = (hit.point - transform.position);
            Quaternion targetRot = Quaternion.LookRotation(lookDelta);
            int turnSpeed = 100;
            float rotSpeed = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotSpeed);
        }


       
      

    }

    private static bool mouseOnLeftEdge()
    {
        return Input.mousePosition.x < Screen.width * 0.49;
    }

    private static bool mouseOnRightEdge()
    {
        return Input.mousePosition.x > Screen.width * 0.51;
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
