using System.Collections;
using UnityEngine;

/// <summary>
/// The drone body component.
/// </summary>
public class Drone : MonoBehaviour {

	/// <summary>
	/// Am I currently in the air?
	/// </summary>
	bool inTheAir = true;

	/// <summary>
	/// Used to see when I've hit the ground.
	/// </summary>
	[SerializeField]
	LayerMask groundLayer;

	/// <summary>
	/// To spawn my descending smoke.
	/// </summary>
	[SerializeField]
	GameObject smokeParticlePrefab;

	/// <summary>
	/// To store a reference to the smoke so I can stop it.
	/// </summary>
	ParticleSystem smoke;

	/// <summary>
	/// When I've hit the ground spawn this.
	/// </summary>
	[SerializeField]
	GameObject explosionParticles;

	/// <summary>
	/// A variable to store a reference to my Rigidbody.
	/// </summary>
	Rigidbody rb;

	/// <summary>
	/// Has Kill been called on me?
	/// </summary>
	bool dying = false;

	/// <summary>
	/// How fast I will spin when I go down.
	/// </summary>
	[SerializeField]
	float rotationSpeed = 200;

	private void Start() {
		rb = gameObject.GetComponent<Rigidbody>();
	}

	/// <summary>
	/// Start my death animation.
	/// </summary>
	public void Kill() {
		if(dying)
			return;

		dying = true;
		rb.isKinematic = false;
		transform.parent = null;
		smoke = Instantiate(smokeParticlePrefab, transform.position, Quaternion.identity, transform).GetComponent<ParticleSystem>();
		transform.Rotate(new Vector3(0, 1, 0), Random.Range(20, 360));
		StartCoroutine(Flight());
	}

	/// <summary>
	/// When I hit something check if it's the ground to finish my death anim off.
	/// </summary>
	/// <param name="coll">The collision I had with another object.</param>
	private void OnCollisionEnter(Collision coll) {
		if(((1 << coll.gameObject.layer) & groundLayer) != 0) {
			inTheAir = false;
			rb.isKinematic = true;
		}
	}

	/// <summary>
	/// Used to control my downwards spiral and after that death explosion.
	/// </summary>
	IEnumerator Flight() {
		float crashAnimSpeed = 0;

		while(inTheAir) {
			yield return null;
			crashAnimSpeed += Time.deltaTime;
			transform.Rotate(new Vector3(0, 1, 0), Time.deltaTime * (crashAnimSpeed * rotationSpeed));
			rb.velocity = transform.forward * (crashAnimSpeed * 10) + Vector3.down * (crashAnimSpeed * 10);
		}

		Instantiate(explosionParticles, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(5);
		smoke.Stop();
		Destroy(gameObject, 5);
	}
}
