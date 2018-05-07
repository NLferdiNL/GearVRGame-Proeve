using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTurret : MonoBehaviour {

    //This script is to track where the camera center looks at and lerp the Turret to it.

    [SerializeField] private float rotationLimit;

    [SerializeField] private GameObject holdCurrentObject; // 

	[SerializeField] private Transform turnPiece;

    [SerializeField] private Transform target; // Target that script owner rotates to.
    [SerializeField] private float speed; // Speed of the rotation.

    void FixedUpdate() // Makes the Lerp follows smoothly.
    {
        TurretRotation(); // Rotate script owner at fixedupdate.
		TurnPieceRotation();
	}

    private void TurretLimit()
    {
        //
    }

    private void TurretRotation() // Lerps this object to look at target.
    {
		Vector3 relativePosition = target.position - transform.position;
        relativePosition = relativePosition.normalized;
        Vector3 targetRotation = Quaternion.LookRotation(relativePosition).eulerAngles;
		targetRotation.x = transform.rotation.eulerAngles.x;
		targetRotation.z = transform.rotation.eulerAngles.z;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), speed * Time.deltaTime);
    }

	private void TurnPieceRotation() {
		Vector3 relativePosition = target.position - turnPiece.position;
		relativePosition = relativePosition.normalized;
		Vector3 targetRotation = Quaternion.LookRotation(relativePosition).eulerAngles;

		targetRotation.y = 0;
		targetRotation.z = 0;

		turnPiece.localRotation = Quaternion.RotateTowards(turnPiece.localRotation, Quaternion.Euler(targetRotation), speed * Time.deltaTime);
	}
}
