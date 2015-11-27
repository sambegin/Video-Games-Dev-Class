using System;
using UnityEngine;

public class CameraObstructionWatcher
{
    public float collidingDistance(Vector3 cameraPosition, Vector3 playerPosition)
    {
        Vector3 directionPlayerToCamera = cameraPosition - playerPosition;
        RaycastHit hit;
        Physics.Raycast(playerPosition, directionPlayerToCamera, out hit);
        return Vector3.Distance(hit.point, playerPosition);
    }
}