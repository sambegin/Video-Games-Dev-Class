using UnityEngine;
using System.Collections;
using System;

public class MouseOrbitImproved : MonoBehaviour
{

    public Transform target;
    public float originalDistance = 5.0f;
    private float distance;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    float x = 0.0f;
    float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        this.distance = this.originalDistance;
    }

    void LateUpdate()
    {
        if (target)
        {
            CameraObstructionWatcher collider = new CameraObstructionWatcher();
            float collidingDistance = collider.collidingDistance(transform.position, target.position);
            
            distance = collidingDistance;
            if (distance > originalDistance)
            {
                distance = originalDistance;
            }

            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}