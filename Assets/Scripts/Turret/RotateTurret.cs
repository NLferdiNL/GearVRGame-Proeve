﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTurret : MonoBehaviour {

    //This script is to track where the camera center looks at and lerp the Turret to it.

	[SerializeField] private Transform turnPiece;
    [SerializeField] private Transform target; // Target that script owner rotates to.

    [SerializeField] private float speed; // Speed of the rotation.

    [SerializeField] private bool isIndependant = false; // Sets whether or not Turret works independant.

    void Start()
    {
        if (isIndependant)
            StartCoroutine(IndependantTurret());
    }

    void FixedUpdate() // Makes the Lerp follows smoothly.
    {
        if (target == null)
            return;

        TurretRotation(); // Rotate Turret at fixedupdate.
		TurnPieceRotation(); // Rotate Turret piece at fixedupdate.
	}

    private void TurretRotation() // Lerps this object to look at target.
    {
		Vector3 relativePosition = target.position - transform.position; // Set relativePosition.
        relativePosition = relativePosition.normalized; // 
        Vector3 targetRotation = Quaternion.LookRotation(relativePosition).eulerAngles; //
		targetRotation.x = transform.rotation.eulerAngles.x; // Targets x axis.
		targetRotation.z = transform.rotation.eulerAngles.z; // Targets z axis.

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), speed * Time.deltaTime); // Rotate Turret
    }

	private void TurnPieceRotation() {
		Vector3 relativePosition = target.position - turnPiece.position;
		relativePosition = relativePosition.normalized;
		Vector3 targetRotation = Quaternion.LookRotation(relativePosition).eulerAngles;

		if(targetRotation.x > 44 && targetRotation.x < 180)
			targetRotation.x = 44;

		targetRotation.y = 0;
		targetRotation.z = 0;

		turnPiece.localRotation = Quaternion.RotateTowards(turnPiece.localRotation, Quaternion.Euler(targetRotation), speed * Time.deltaTime);
	}

    private IEnumerator IndependantTurret()
    {
        while(isIndependant)
        {
            yield return new WaitUntil(() => target == null);

            target = SwarmContainer.RandomEnemy;

            if (target == null)
                yield return new WaitUntil(() => SwarmContainer.EnemiesAvailable);
        }
    }
}
