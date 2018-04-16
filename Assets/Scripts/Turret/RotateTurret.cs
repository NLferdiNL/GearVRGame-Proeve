using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTurret : MonoBehaviour {

    //This script is to track where the camera center looks at and lerp the Turret to it.

    private Vector3 _relativePosition; // Holds position of target.
    private Quaternion _targetRotation;
    private Vector3 _rotationLimit;

    [SerializeField] private GameObject _holdCurrentObject;

    public Transform target; // Target that script owner rotates to.
    public float speed; // Speed of the rotation.

    //float rotationTime;

    void Start()
    {
        //_rotationLimit = new Vector3(1,1,1);

    }

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
        _relativePosition = target.position - transform.position;
        _targetRotation = Quaternion.LookRotation(_relativePosition);

        //rotationTime = speed;
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, speed);
    }
}
