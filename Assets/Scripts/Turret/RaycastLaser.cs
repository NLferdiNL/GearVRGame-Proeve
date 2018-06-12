using System.Collections;
using UnityEngine;

public class RaycastLaser : MonoBehaviour
{
    //This script creates the laser and the controls for space and touch.
    [SerializeField] private SoundManager SM;

    [SerializeField] private LayerMask hitMask;

    [SerializeField] private AudioSource laserOn;
    
    LineRenderer laserLine; // Line for the laser.

	[SerializeField] private float damagePerFrame = 1;
    public float range; // Sets Range of Line.

    private bool isShooting = true;

    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.enabled = false;
    }

    void Start() // Gets the laserLine component and enables it true at start of the scene.
    {
        StartCoroutine(LaserOn());
    }

    IEnumerator LaserOn()
    {
        yield return new WaitForSecondsRealtime(12);
        laserLine.enabled = true;
        laserOn.clip = SM.SfxHolder[0];
    }

    void FixedUpdate() // Checks if person hit space or touched on phone and runs Shoot();
    {
        Shoot();

    }
    void DisableEffects() // Disables the laserLine.
    {
        laserLine.enabled = false;
        isShooting = false;
    }
    void EnableEffects() // Enables the laserLine.
    {
        laserLine.enabled = true;
        isShooting = true;
    }

    void Shoot() // Creates the line visable in game if laserLine.enabled = true.
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        laserLine.SetPosition(0, transform.position);


        if (Physics.Raycast(ray, out hit, range, hitMask))
        {
            laserLine.SetPosition(1, hit.point);
            hit.collider.gameObject.SendMessage("Heal", damagePerFrame, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            laserLine.SetPosition(1, ray.origin + ray.direction * range);
        }
    }

}
