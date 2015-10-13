using UnityEngine;
using System.Collections;
using System;

public class Weapon : MonoBehaviour
{

    LineRenderer lineRenderer;
    //EllipsoidParticleEmitter particleEmitter;

    // Use this for initialization
    void Start()
    {
        this.lineRenderer = GetComponent<LineRenderer>();
        this.lineRenderer.enabled = false;
        //this.particleEmitter = GetComponent<EllipsoidParticleEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StopCoroutine("shoot");
            StartCoroutine("shoot");
        }
    }

    IEnumerator shoot()
    {
        lineRenderer.enabled = true;
        while (Input.GetButton("Fire1"))
        {
            RaycastHit hitSpot;
            //Vector3 forwardDirection = transform.up;
            bool asHitOnSomething = Physics.Raycast(transform.position, transform.forward, out hitSpot);
            
            
            Ray ray = new Ray(transform.position, transform.forward);
            if (asHitOnSomething)
            {
                lineRenderer.SetPosition(1, hitSpot.point);

            }
            else
            {
                //TODO: Not necessarly in straight line.
                int farAway = 1000;
                lineRenderer.SetPosition(1, ray.GetPoint(farAway));
            }
            lineRenderer.SetPosition(0, transform.position);

            Debug.DrawRay(ray.origin, ray.direction);
            //this.particleEmitter.emit = true;
            yield return null;
        }

        stopShooting();


    }

    private void stopShooting()
    {
        lineRenderer.enabled = false;
        //this.particleEmitter.emit = false;
    }


}
