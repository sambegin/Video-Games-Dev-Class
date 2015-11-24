using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {

    public Transform target;
    private float distance = 10.0f;
    private float height = 5.0f;

    private float heightDamping = 2.0f;
    private float rotationDamping = 3.0f;

	
	// Update is called once per frame
	void LateUpdate ()
    {
	    if (target)
        {
            // Calculate the current rotation angles
            var wantedRotationAngle = target.eulerAngles.y;
            var wantedHeight = target.position.y + height;

            var currentRotationAngle = transform.eulerAngles.y;
            var currentHeight = transform.position.y;

            // Damp the rotation around the y-axis
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            // Convert the angle into a rotation
            var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            var position = target.position;
            position -= currentRotation * Vector3.forward * distance;
            position.y = currentHeight;

            // Set the height of the camera
            //transform.position.y = currentHeight;
            transform.position = position;
            // Always look at the target
            transform.LookAt(target);
        }    
	}
}
