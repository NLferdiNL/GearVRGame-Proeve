using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTurret : MonoBehaviour {

    //This script is to track where the camera center looks at and lerp the Turret to it.

    Vector3 relativePosition;
    Quaternion targetRotation;

    public Transform target;
    public float speed = 0.001f;

    float rotationTime;

    void FixedUpdate() // Makes the Lerp follows smoothly.
    {
        TurretRotation();
    }

    private void TurretRotation() // Lerps this object to look at target.
    {
        relativePosition = target.position - transform.position;
        targetRotation = Quaternion.LookRotation(relativePosition);

        rotationTime += Time.deltaTime * speed;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationTime);
    }
}
