using UnityEngine;
using System.Collections;
using System;

public class Weapon : MonoBehaviour
{

    LineRenderer lineRenderer;

    // Use this for initialization
    void Start()
    {
        this.lineRenderer = GetComponent<LineRenderer>();
        this.lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }

        if (Input.GetMouseButtonUp(0))
        {
            stopShooting();
        }
    }

    private void shoot()
    {
        RaycastHit hitSpot;
        Vector3 forwardDirection = transform.up;
        bool asHitOnSomething = Physics.Raycast(transform.position, forwardDirection, out hitSpot);
        if (asHitOnSomething)
        {
            lineRenderer.SetPosition(1, hitSpot.point);
        }
        else
        {
            lineRenderer.SetPosition(1, forwardDirection*10);
        }
        lineRenderer.SetPosition(0, transform.position);


        lineRenderer.enabled = true;
    }

    private void stopShooting()
    {
        lineRenderer.enabled = false;
    }


}
