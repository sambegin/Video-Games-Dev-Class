﻿using UnityEngine;
using System.Collections;
using System;

public class MouseOrbitWithZoomCamera : MonoBehaviour {

    private Transform cameraTarget;
    public float distance = 10;
    private float originalDistance;
    public float xSpeed = 250;
    public float ySpeed = 120;
    public float yMinLimit = -10;
    public float yMaxLimit = 50;

    private float x = 0;
    private float y = 0;

    private CameraObstructionWatcher cameraObstructionWatcher;

	void Start () {
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        cameraTarget = GameObject.FindGameObjectWithTag("CameraTarget").GetComponent<Transform>();
        cameraObstructionWatcher = new CameraObstructionWatcher();
        originalDistance = distance; 
    }
	
	void LateUpdate () {
        if (cameraTarget)
        {
            float collidingDistance = cameraObstructionWatcher.collidingDistance(transform.position, cameraTarget.position);
            distance = collidingDistance < originalDistance ? collidingDistance : originalDistance;

            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            var rotation = Quaternion.Euler(y, x, 0);
            var position = rotation * new Vector3(0, 0, -distance) + cameraTarget.position + cameraTarget.right + cameraTarget.up;

            transform.rotation = rotation;
            transform.position = position;
        }
	}

    private float ClampAngle(float angle, float yMinLimit, float yMaxLimit)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, yMinLimit, yMaxLimit);
    }

}
