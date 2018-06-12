using System.Collections;
using UnityEngine;
/// <summary>
/// This script creates the laser and the controls for space and touch.
/// </summary>
public class RaycastLaser : MonoBehaviour
{
    [SerializeField] private SoundManager sM; // Hold SoundManager.

    [SerializeField] private LayerMask hitMask; // Hitmask of the laser.

    [SerializeField] private AudioSource laserOn; // Audio of laser turning on.
    
    LineRenderer laserLine; // Line for the laser.

	[SerializeField] private float damagePerFrame = 1; // Damage it does per frame.
    public float range; // Sets Range of Line.

    private bool isShooting = true; // Bool to check if shooting.
    /// <summary>
    /// Set laser line and make sure it's turned off.
    /// </summary>
    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.enabled = false;
    }
    /// <summary>
    ///  Gets the laserLine component and enables it true at start of the scene.
    /// </summary>
    void Start()
    {
        StartCoroutine(LaserOn());
    }
    /// <summary>
    /// Wait until tutorial part is done and turn on the laser and play laserOn sfx.
    /// </summary>
    /// <returns> Wait until tutorial part is done.</returns>
    IEnumerator LaserOn()
    {
        yield return new WaitForSecondsRealtime(12);
        laserLine.enabled = true;
        laserOn.clip = sM.SfxHolder[6];
    }
    /// <summary>
    ///  runs Shoot();
    /// </summary>
    void FixedUpdate()
    {
        Shoot();

    }
    /// <summary>
    /// Disable the laser.
    /// </summary>
    void DisableEffects()
    {
        laserLine.enabled = false;
        isShooting = false;
    }
    /// <summary>
    /// Enable the laser.
    /// </summary>
    void EnableEffects()
    {
        laserLine.enabled = true;
        isShooting = true;
    }
    /// <summary>
    /// Creates the line visable in game if laserLine.enabled = true.
    /// </summary>
    void Shoot()
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
