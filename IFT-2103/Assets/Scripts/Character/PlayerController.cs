using UnityEngine;
using System;
using System.Collections;


public class PlayerController : MonoBehaviour
{

    private float speed = 20.0f;
    private float jumpHeight = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 50.0f;

    private bool flashlightIsOn;
    private Light flashlight;
    private float initialFlashlightIntensity;

    public float rotSpeed;

    private Camera mainCamera;
    private Transform weapon;
    private Transform hitTarget;

    private Animator animator;

    void Start()
    {
        this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        this.weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Transform>();
        this.hitTarget = GameObject.FindGameObjectWithTag("HitTarget").GetComponent<Transform>();
        this.flashlight = GetComponentInChildren<Light>();
        this.animator = GetComponentInChildren<Animator>();
        initialFlashlightIntensity = flashlight.intensity;
        flashlight.intensity = 0;


        this.animator.SetBool("walking", true);
    }

    void Update()
    {
        handleCharacterBodyControl();
        handleCharacterWeaponControl();
        handleFlashlight();
    }

    void FixedUpdate()
    {
        handleCharacterJump();
    }

    private void handleFlashlight()
    {
        AudioSource[] audio = GetComponents<AudioSource>();
        if (Input.GetKeyDown(KeyCode.Q) && flashlightIsOn)
        {
            audio[0].Play();
            flashlight.intensity = 0;
            flashlightIsOn = false;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && !flashlightIsOn)
        {
            audio[0].Play();
            flashlight.intensity = initialFlashlightIntensity;
            flashlightIsOn = true;
        }
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
        AudioSource[] audio = GetComponents<AudioSource>();

        CharacterController controller = GetComponent<CharacterController>();

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                audio[1].Play();
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

