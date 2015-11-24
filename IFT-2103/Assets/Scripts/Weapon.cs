using UnityEngine;
using System.Collections;
using System;

public class Weapon : MonoBehaviour
{
    LineRenderer lineRenderer;
    Transform rayTarget;
    public Transform cubeTarget;
    private Transform weapon;
    //Camera mainCamera;
    //TODO: Make laser more realistic
    //EllipsoidParticleEmitter particleEmitter;

    void Start()
    {
        this.lineRenderer = GetComponent<LineRenderer>();
        this.lineRenderer.enabled = false;
        this.rayTarget = GameObject.FindGameObjectWithTag("Target").GetComponent<Transform>();
        this.weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Transform>();
        // this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

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
            Ray ray = new Ray(cubeTarget.position, cubeTarget.forward);

            lineRenderer.SetPosition(0, weapon.position);
            lineRenderer.SetPosition(1, cubeTarget.position);
            
            bool asHitOnSomething = Physics.Raycast(transform.position, transform.forward, out hitSpot);

            //lineRenderer.SetPosition(1, hitSpot.point);


            //Ray ray = new Ray(transform.position, transform.forward);
            //Ray ray = new Ray(cubeTarget.position, cubeTarget.forward);
            //if (asHitOnSomething)
            //{
            //    Lightable objectShot = hitSpot.transform.GetComponent<Lightable>();

            //    if (objectShot != null)
            //    {
            //        objectShot.lightUp(hitSpot);
            //    }


            //}
            //else
            //{
            //    int farAway = 1000;
            //    lineRenderer.SetPosition(1, ray.GetPoint(farAway));
            //}
            //lineRenderer.SetPosition(0, transform.position);

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
