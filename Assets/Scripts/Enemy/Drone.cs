using System.Collections;
using UnityEngine;

// The drone body component.
public class Drone : MonoBehaviour {

	bool inTheAir = true;

	// Used to see when I've hit the ground.
	[SerializeField]
	LayerMask groundLayer;

	// To spawn my descending smoke.
	[SerializeField]
	GameObject smokeParticlePrefab;

	// To store a reference to the smoke so I can stop it.
	ParticleSystem smoke;

	// When I've hit the ground spawn this.
	[SerializeField]
	GameObject explosionParticles;

	Rigidbody rb;

	// Has Kill been called on me?
	bool dying = false;

	// How fast I will spin when I go down.
	[SerializeField]
	float rotationSpeed = 200;

	private void Start() {
		rb = gameObject.GetComponent<Rigidbody>();
	}

	// Start my death animation.
	public void Kill() {
		if(dying)
			return;

		dying = true;
		rb.isKinematic = false;
		transform.parent = null;
		smoke = Instantiate(smokeParticlePrefab, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
		transform.Rotate(new Vector3(0, 1, 0), Random.Range(20, 360));
		StartCoroutine(Flight());
	}

	// When I hit something check if it's the ground to finish my death anim off.
	private void OnCollisionEnter(Collision coll) {
		if(((1 << coll.gameObject.layer) & groundLayer) != 0) {
			inTheAir = false;
			rb.isKinematic = true;
		}
	}

	// Used to control my downwards spiral and after that death explosion.
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
