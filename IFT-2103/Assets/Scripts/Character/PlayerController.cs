using UnityEngine;
using System;
using System.Collections;


public class PlayerController : MonoBehaviour
{

    private float speed = 20.0f;
    private float jumpHeight = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 50.0f;
    private float inAirDrift = 10.0f;

    public float rotSpeed;


    private Camera mainCamera;
    private Transform weapon;
    private Transform hitTarget;

    void Start ()
    {
        this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        this.weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Transform>();
        this.hitTarget = GameObject.FindGameObjectWithTag("HitTarget").GetComponent<Transform>();
    }

    void Update()
    {
        handleCharacterBodyControl();
        handleCharacterWeaponControl();
        handleCharacterJump();
    }

    private void handleCharacterBodyControl()
    {
        var targetPos = mainCamera.transform.position;
        targetPos.y = transform.position.y; //Allows the hole body to stay completely straight. We now don't have the "weird" effect.
        var targetDir = Quaternion.LookRotation(-(targetPos - transform.position));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetDir, rotSpeed * Time.deltaTime);
    }

    private void handleCharacterWeaponControl()
    {
        weapon.LookAt(hitTarget.position);
    }

    private void handleCharacterJump()
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

