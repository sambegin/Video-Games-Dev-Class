using UnityEngine;
using System.Collections;

public class HitSpot : MonoBehaviour {

    public Camera mainCamera;
    public float HitSpotDistance = 20;

	void Update () {
        Vector3 centerOfCameraScreen = new Vector3(0.5f, 0.5f, 0);
        Vector3 hitSpotPoint = mainCamera.ViewportPointToRay(centerOfCameraScreen).GetPoint(HitSpotDistance);
        transform.position = hitSpotPoint;
	}
}
