using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastLaser : MonoBehaviour {

    public float range = 16f; // Sets Range of Line.

    Ray shootRay; // Gets the Ray.
    //RaycastHit shootHit;
    //int shootableMask;

    LineRenderer laserLine; // Line for the laser.

    bool isShooting = true;

    void Awake() // Gets the laserLine component and enables it true at start of the scene.
    {
        //shootableMask = LayerMask.GetMask("Shootable");
        laserLine = GetComponent<LineRenderer>();
        laserLine.enabled = true;
        laserLine.SetWidth(0.1f, 0.25f);
        //
    }

    void Update() // Checks if person hit space or touched on phone and runs Shoot();
    {
        Debug.Log(isShooting);
        if (Input.GetKeyDown("space"))
        {
            if (isShooting)
            {
                DisableEffects();
            }
            else if(isShooting == false) {
                EnableEffects();
            }
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (isShooting)
                {
                    DisableEffects();
                }
                else if (isShooting == false) {
                    EnableEffects();
                }
            }
        }
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
        //laserLine.enabled = true;
        laserLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        laserLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
    }

}
