﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTurret : MonoBehaviour {

    //This script is to track where the camera center looks at and lerp the Turret to it.

    private Vector3 relativePosition; // Holds position of target.
    private Quaternion targetRotation;
    private Vector3 rotationLimit;

    [SerializeField] private GameObject holdCurrentObject;

    [SerializeField] private Transform target; // Target that script owner rotates to.
    [SerializeField] private float speed; // Speed of the rotation.

    void FixedUpdate() // Makes the Lerp follows smoothly.
    {
        TurretRotation(); // Rotate script owner at fixedupdate.
    }

    private void TurretLimit()
    {
        //
    }

    private void TurretRotation() // Lerps this object to look at target.
    {
        relativePosition = target.position - transform.position;
        targetRotation = Quaternion.LookRotation(relativePosition);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed);
    }
}
