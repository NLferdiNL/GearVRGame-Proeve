using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTurret : MonoBehaviour {

    //This script is to track where the camera center looks at and lerp the Turret to it.

    Vector3 relativePosition; // Holds position of target.
    Quaternion targetRotation;

    public Transform target; // Target that script owner rotates to.
    public float speed; // Speed of the rotation.

    //float rotationTime;

    void FixedUpdate() // Makes the Lerp follows smoothly.
    {
        TurretRotation(); // Rotate script owner at fixedupdate.
    }

    private void TurretRotation() // Lerps this object to look at target.
    {
        relativePosition = target.position - transform.position;
        targetRotation = Quaternion.LookRotation(relativePosition);

        //rotationTime = speed;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed);
    }
}
