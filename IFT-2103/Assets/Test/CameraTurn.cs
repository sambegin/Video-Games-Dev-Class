using UnityEngine;
using System;
using System.Collections;


public class CameraTurn : MonoBehaviour
{
    public Camera mainCamera;
    public float rotSpeed;
    public Transform test;
    public float TestDistance = 20;

    private Transform weapon;
    private Transform weaponTarget;

    void Start ()
    {
        this.weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Transform>();
        this.weaponTarget = GameObject.FindGameObjectWithTag("Target").GetComponent<Transform>();
    }

    void Update()
    {
        var targetPos = mainCamera.transform.position;

        targetPos.y = transform.position.y; //set targetPos y equal to mine, so I only look at my own plane

        var targetDir = Quaternion.LookRotation(-(targetPos - transform.position));

        transform.rotation = Quaternion.Slerp(transform.rotation, targetDir, rotSpeed * Time.deltaTime);




        //Vector3 centerScreenVector = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        //Vector3 vectorBetweenCameraAndCenterOfScreen = centerScreenVector - mainCamera.transform.position;

        //test.position = -vectorBetweenCameraAndCenterOfScreen;

        Vector3 viewPortCenter = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Vector3 testssss = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)).GetPoint(TestDistance);
        test.position = testssss;
        weapon.LookAt(testssss);
    }
}

