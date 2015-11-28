using System;
using UnityEngine;

public class CameraObstructionWatcher
{
    public float collidingDistance(Vector3 cameraPosition, Vector3 cameraTargetPosition)
    {
        RaycastHit hit = getRayCastHit(cameraPosition, cameraTargetPosition);
        return Vector3.Distance(hit.point, cameraTargetPosition);
    }

    private static RaycastHit getRayCastHit(Vector3 cameraPosition, Vector3 playerPosition)
    {
        Vector3 directionPlayerToCamera = cameraPosition - playerPosition;
        RaycastHit hit;
        Physics.Raycast(playerPosition, directionPlayerToCamera, out hit);
        return hit;
    }

    public Collider GetCollidingObject(Vector3 cameraPosition, Vector3 cameraTargetPosition)
    {
        RaycastHit hit = getRayCastHit(cameraPosition, cameraTargetPosition);
        return hit.collider;
    }
}