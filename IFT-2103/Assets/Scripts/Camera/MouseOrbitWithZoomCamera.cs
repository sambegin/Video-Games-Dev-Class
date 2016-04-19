using UnityEngine;
using System.Collections;
using System;

public class MouseOrbitWithZoomCamera : MonoBehaviour
{

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

    void Start()
    {
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        cameraTarget = GameObject.FindGameObjectWithTag("CameraTarget").GetComponent<Transform>();
        cameraObstructionWatcher = new CameraObstructionWatcher();
        originalDistance = distance;
    }

#if !UNITY_ANDROID
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
#endif

#if UNITY_ANDROID
    void LateUpdate()
    {
        if (cameraTarget && Input.touchCount > 0 && notOnTheButtonsZone(Input.touches[0]))
        {
            float collidingDistance = cameraObstructionWatcher.collidingDistance(transform.position, cameraTarget.position);
            distance = collidingDistance < originalDistance ? collidingDistance : originalDistance;

            x += Input.touches[0].deltaPosition.x * xSpeed * 0.02f;
            y -= Input.touches[0].deltaPosition.y * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            var rotation = Quaternion.Euler(y, x, 0);
            var position = rotation * new Vector3(0, 0, -distance) + cameraTarget.position + cameraTarget.right + cameraTarget.up;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    bool notOnTheButtonsZone(Touch touch)
    {
        y = touch.position.y;
        double buttonsZoneTopLimit = Screen.height * 0.35;
        if (y < buttonsZoneTopLimit)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

#endif

    private float ClampAngle(float angle, float yMinLimit, float yMaxLimit)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, yMinLimit, yMaxLimit);
    }

}
