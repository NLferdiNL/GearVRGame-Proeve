using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastLaser : MonoBehaviour {

    //This script creates the laser and the controls for space and touch.

    public float range; // Sets Range of Line.

    [SerializeField]LayerMask hitMask;

    LineRenderer laserLine; // Line for the laser.

    bool isShooting = true;

    void Awake() // Gets the laserLine component and enables it true at start of the scene.
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.enabled = true;
        //range = 18; y u do dis?
        //laserLine.SetWidth(0.1f, 0.25f);
        //
    }
    void Update()
    {
        TempControls();
    }

    void TempControls()
    {
        if (Input.GetKeyDown("space"))
        {
            if (isShooting)
            {
                DisableEffects();
            }
            else if (isShooting == false)
            {
                EnableEffects();
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (isShooting)
                {
                    DisableEffects();
                }
                else if (isShooting == false)
                {
                    EnableEffects();
                }
            }
        }
    }

    void FixedUpdate() // Checks if person hit space or touched on phone and runs Shoot();
    {
        Shoot();

    }
    void DisableEffects() // Disables the laserLine.
    {
        laserLine.enabled = false;
        isShooting = false;
    }
    void EnableEffects() // Enables the laserLine.
    {
        laserLine.enabled = true;
        isShooting = true;
    }

    void Shoot() // Creates the line visable in game if laserLine.enabled = true.
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        laserLine.SetPosition(0, transform.position);


        if (Physics.Raycast(ray, out hit, range, hitMask))
        {
            laserLine.SetPosition(1, hit.point);
            hit.collider.gameObject.SendMessage("Heal", 1, SendMessageOptions.DontRequireReceiver);
        }
        else {

            //laserLine.enabled = true;

            laserLine.SetPosition(1, ray.origin + ray.direction * range);
        }
    }

}
