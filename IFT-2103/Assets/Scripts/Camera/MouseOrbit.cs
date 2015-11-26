using UnityEngine;
using System.Collections;
using System;

public class MouseOrbit : MonoBehaviour {

    private Transform cameraTarget;
    public float distance = 10;
    public float xSpeed = 250;
    public float ySpeed = 120;
    public float yMinLimit = -20;
    public float yMaxLimit = 50;

    private float x = 0;
    private float y = 0;


	// Use this for initialization
	void Start () {
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        cameraTarget = GameObject.FindGameObjectWithTag("CameraTarget").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (cameraTarget)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            var rotation = Quaternion.Euler(y, x, 0);
            var position = rotation * new Vector3(0, 0, -distance) + cameraTarget.position;

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
