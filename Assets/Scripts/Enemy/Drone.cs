using System.Collections;
using UnityEngine;

public class Drone : MonoBehaviour {

	bool inTheAir = true;

	[SerializeField]
	LayerMask groundLayer;

	[SerializeField]
	GameObject smokeParticles;

	ParticleSystem smoke;

	[SerializeField]
	GameObject explosionParticles;

	Rigidbody rb;

	bool dying = false;

	float rotationSpeed = 200;

	private void Start() {
		rb = gameObject.GetComponent<Rigidbody>();
	}

	public void Kill() {
		if(dying)
			return;

		dying = true;
		rb.isKinematic = false;
		transform.parent = null;
		smoke = Instantiate(smokeParticles, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
		transform.Rotate(new Vector3(0, 1, 0), Random.Range(20, 360));
		StartCoroutine(Flight());
	}

	private void OnCollisionEnter(Collision coll) {
		if(((1 << coll.gameObject.layer) & groundLayer) != 0) {
			inTheAir = false;
			rb.isKinematic = true;
		}
	}

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
