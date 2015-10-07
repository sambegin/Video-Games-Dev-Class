using UnityEngine;
using System.Collections;
using System;

public class FollowCamera : MonoBehaviour {

    GameObject target;
    Vector3 cameraOffset;
    private bool hasCollided;
    private Vector3 cameraPositionFromPlayer;

    void Start () {
        this.hasCollided = false;
	}
	
	void Update () {
	
	}

    internal void setTarget(GameObject target)
    {
        this.target = target;
        cameraPositionFromPlayer = new Vector3(0, 2, -5);
        this.cameraOffset = target.transform.position - cameraPositionFromPlayer;
    }

    public void LateUpdate()
    {
        if (hasCollided)
        {
            verifyIfNotCollidingAnymore();
        }
        else
        {
            float desiredAngle = target.transform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
            transform.position = target.transform.position - (rotation * cameraOffset);
        }
        transform.LookAt(target.transform);
    }



    private void verifyIfNotCollidingAnymore()
    {
        float actualDistance = Vector3.Distance(transform.position, target.transform.position);
        float supposedDistance = Vector3.Magnitude(cameraPositionFromPlayer);

        if(actualDistance > supposedDistance)
        {
            hasCollided = false;
        }
    }

    public void OnTriggerEnter()
    {
        this.hasCollided = true;
    }

    }
