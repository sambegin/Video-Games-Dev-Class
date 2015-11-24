using UnityEngine;
using System.Collections;

public class SmoothLookAt : MonoBehaviour {

    public Transform target;
    private float damping = 6;
    public bool Smooth = true; 

	void LateUpdate ()
    {
	    if (target)
        {
            if (Smooth)
            {
                var rotation = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            }
            else
            {
                transform.LookAt(target);
            }
        }
	}
}
