using System.Collections;
using UnityEngine;
/// <summary>
/// This script is to track where the camera center looks at and lerp the Turret to it.
/// </summary>
public class RotateTurret : MonoBehaviour
{
	[SerializeField] private Transform turnPiece; // TurnPiece of the turret.
    [SerializeField] private Transform target; // Target that script owner rotates to.

    [SerializeField] private float speed; // Speed of the rotation.

    [SerializeField] private bool isIndependant = false; // Sets whether or not Turret works independant.

    [SerializeField] private AudioSource turretMove;
    /// <summary>
    /// If the turret is independant call IndependantTurret Ienumerator.
    /// </summary>
    void Start()
    {
        if (isIndependant)
            StartCoroutine(IndependantTurret());
    }
    /// <summary>
    /// Makes the Lerp follows smoothly.
    /// </summary>
    void FixedUpdate()
    {
        if (target == null)
            return;

        TurretRotation(); // Rotate Turret at fixedupdate.
		TurnPieceRotation(); // Rotate Turret piece at fixedupdate.
	}
    /// <summary>
    /// // Lerps this object to look at target.
    /// </summary>
    void TurretRotation()
    {
        Vector3 relativePosition = target.position - transform.position;
        relativePosition = relativePosition.normalized;
        Vector3 targetRotation = Quaternion.LookRotation(relativePosition).eulerAngles;
		targetRotation.x = transform.rotation.eulerAngles.x;
		targetRotation.z = transform.rotation.eulerAngles.z;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), speed * Time.deltaTime); // Rotate Turret
    }
    /// <summary>
    /// Rotate the turnpiece of the laser.
    /// </summary>
    void TurnPieceRotation() {
		Vector3 relativePosition = target.position - turnPiece.position;
		relativePosition = relativePosition.normalized;
		Vector3 targetRotation = Quaternion.LookRotation(relativePosition).eulerAngles;

		if(targetRotation.x > 44 && targetRotation.x < 180)
			targetRotation.x = 44;

		targetRotation.y = 0;
		targetRotation.z = 0;

		turnPiece.localRotation = Quaternion.RotateTowards(turnPiece.localRotation, Quaternion.Euler(targetRotation), speed * Time.deltaTime);
	}
    /// <summary>
    /// Makes the Turret independantly able to target enemies automatically if isIndependant is true.
    /// </summary>
    /// <returns></returns>
    IEnumerator IndependantTurret()
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
