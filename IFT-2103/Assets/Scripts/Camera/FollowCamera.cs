using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

    GameObject target;
    Vector3 cameraOffset;
    private bool isColliding;

    // Use this for initialization
    void Start () {
        this.isColliding = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void setTarget(GameObject target)
    {
        this.target = target;
        Vector3 cameraPositionFromPlayer = new Vector3(0, 2, -5);
        this.cameraOffset = target.transform.position - cameraPositionFromPlayer;
    }

    void LateUpdate()
    {
        if (!isColliding)
        {
            float desiredAngle = target.transform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
            transform.position = target.transform.position - (rotation * cameraOffset);
            transform.LookAt(target.transform);
        }
        isColliding = false;

    }

}
