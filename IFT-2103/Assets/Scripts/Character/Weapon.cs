using UnityEngine;
using System.Collections;
using System;

public class Weapon : MonoBehaviour
{
    LineRenderer lineRenderer;
    private Transform hitTarget;
    private Transform weapon;

    void Start()
    {
        this.lineRenderer = GetComponent<LineRenderer>();
        this.lineRenderer.enabled = false;
        this.weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Transform>();
        this.hitTarget = GameObject.FindGameObjectWithTag("HitTarget").GetComponent<Transform>();
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
        AudioSource audio = GetComponentInParent<AudioSource>();
        lineRenderer.enabled = true;
        audio.Play();
        while (Input.GetButton("Fire1"))
        {
            RaycastHit hitSpot;
            bool hasHitOnSomething = Physics.Raycast(transform.position, transform.forward, out hitSpot);

            if (hasHitOnSomething)
            {
                reduceLaserToCollidedObjectPosition(hitSpot);
                lightUpCollidedObject(hitSpot);
            }
            else
            {
                displayFullLengthOfLaser();
            }
            lineRenderer.SetPosition(0, weapon.position); //At any time, laser has come from weapon
            yield return null;
        }
        audio.Stop();
        stopShooting();
    }

    private void displayFullLengthOfLaser()
    {
        Ray ray = new Ray(hitTarget.position, weapon.forward);
        int laserLength = 500; //Could be modified by a model ... (IE. changing for a stronger weapon)
        lineRenderer.SetPosition(1, ray.GetPoint(laserLength));
    }

    private static void lightUpCollidedObject(RaycastHit hitSpot)
    {
        Lightable objectShot = hitSpot.transform.GetComponent<Lightable>();
        if (objectShot != null)
        {
            objectShot.lightUp(hitSpot);
        }
    }

    private void reduceLaserToCollidedObjectPosition(RaycastHit hitSpot)
    {
        lineRenderer.SetPosition(1, hitSpot.point);
    }

    private void stopShooting()
    {
        lineRenderer.enabled = false;
        //this.particleEmitter.emit = false;
    }


}
