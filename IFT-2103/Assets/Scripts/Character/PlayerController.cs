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
    CharacterController controller;
    private AudioSource[] audio;

    void Start()
    {
        this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        this.weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Transform>();
        this.hitTarget = GameObject.FindGameObjectWithTag("HitTarget").GetComponent<Transform>();
        this.flashlight = GetComponentInChildren<Light>();
        this.animator = GetComponentInChildren<Animator>();
        initialFlashlightIntensity = flashlight.intensity;
        flashlight.intensity = 0;
        this.controller = GetComponent<CharacterController>();
        this.audio = GetComponents<AudioSource>();
    }

    void Update()
    {
        handleCharacterBodyControl();
        handleCharacterWeaponControl();
        handleFlashlight();
    }

    void FixedUpdate()
    {
        handleCharacterTranslations();
        handleCharacterJump();
    }

    private void handleCharacterJump()
    {
        if (controller.isGrounded && Input.GetButton("Jump"))
        {
            jump();
        }
    }

    private void handleFlashlight()
    {
        bool hasToggledFlashlight = false;
        #if UNITY_ANDROID
        float zAcceleration = Input.acceleration.z;
        if (zAcceleration > 1)
        {
            hasToggledFlashlight = true;
        }
        #endif
        if (Input.GetKeyDown(KeyCode.Q))
        {
            hasToggledFlashlight = true;
        }
        if (hasToggledFlashlight && flashlightIsOn)
        {
            turnFlashlightOn();
        }
        else if (hasToggledFlashlight && !flashlightIsOn)
        {
            turnFlashLightOff();
        }
    }

    private void turnFlashLightOff()
    {
        audio[0].Play();
        flashlight.intensity = initialFlashlightIntensity;
        flashlightIsOn = true;
    }

    private void turnFlashlightOn()
    {
        audio[0].Play();
        flashlight.intensity = 0;
        flashlightIsOn = false;
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

    private void handleCharacterTranslations()
    {       
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
        else
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.x *= speed;
            moveDirection.z *= speed;
        }

        moveDirection.y -= gravity * Time.deltaTime;

        if (moveDirection.magnitude == 1)
        {
            this.animator.SetBool("walking", false);
        }
        else
        {
            this.animator.SetBool("walking", true);
            controller.Move(moveDirection * Time.deltaTime);
        }

    }

    private void jump()
    {
        audio[1].Play();
        moveDirection.y = jumpHeight;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void jumpFromUI()
    {
        if (controller.isGrounded)
        {
            Debug.Log("jump from mobile");
            jump();
        }
    }

    public void flashlightToggle()
    {

    }

}

