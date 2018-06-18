using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This Script basicly holds the current lvl of Power for the animator to work with.
/// Using that lvlOfPower in order to influence the animator of both the building itself and the radarDot
/// </summary>
public class Building : MonoBehaviour, IDamagable
{
    // These hold the two animators i use one for the animator on the building
    [SerializeField]
    public Animator buildingAnimator, radarDotAnimator;

    // This is the amount of power the building currently has and is being changed by the enemy's and turrets 
    [SerializeField]
    private float lvlOfPower = 0;

    // This is used as a limiter for the amount of power. 
    // This is needed for the animator.
    [SerializeField]
    private float maxLvlOfPower = 100;

    // This is a variable that sets the amount of time the building stays under attack, 
    // after the enemy has done damage to the building. 
    [SerializeField]
    float underAttackCooldown = 2f;

    // This variable cooperates with "underAttackCooldown" to go in and out of being attacked.
    [SerializeField]
    float timeSinceLastAttack = 0;
	
    // This soundManager handles the soundeffects for the building.
    [SerializeField]
    private SoundManager sM;

    // This is where the "AudioSource" of the building itself is placed.
    private AudioSource buildingSfx;

    // This is a Unity event that is triggered when the building is completely charged. 
    [Serializable]
    public class BuildingFullyChargedEvent : UnityEvent { }

    public BuildingFullyChargedEvent OnFullCharge = new BuildingFullyChargedEvent();

    // This is a Unity event that is triggered when the building is completely empty. 
    [Serializable]
    public class BuildingEmptyChargedEvent : UnityEvent { }

    public BuildingEmptyChargedEvent OnEmptyCharge = new BuildingEmptyChargedEvent();

    // This bool gets set on true when the building has full power.
    bool fullyHealed = false;

    
    public float LvlOfPower
    {
        get
        {
            return lvlOfPower;
        }
        set
        {
            lvlOfPower += value;
        }
    }

    public float MaxHealth
    {
        get
        {
            return (int)maxLvlOfPower;
        }
    }

    public float Health
    {
        get
        {
            return (int)lvlOfPower;
        }
    }

    /// <summary>
    /// The get set for when it is under attack.
    /// </summary>
    public bool UnderAttack
    {
        get
        {
            return timeSinceLastAttack < underAttackCooldown;
        }
    }

    /// <summary>
    /// In this "Start" function I get the components,
    /// the "Animator" and the "AudioSource" for later use
    /// </summary>
    void Start()
    {
        buildingSfx = GetComponent<AudioSource>();

        if (buildingAnimator == null)
        {
            buildingAnimator = GetComponentInParent<Animator>();
        }
        SoundController.Instance.OnReset.AddListener(SwitchFase);
    }

    /// <summary>
    /// This "FixedUpdate" is mainly used for updating the "radarDot" state 
    /// (when it needs to be on or off).
    /// The "lvlOfPower" also gets updated in the "FixedUpdate" for the smoothest transition.
    /// </summary>
    void FixedUpdate()
    {
        if (timeSinceLastAttack < underAttackCooldown)
        {
            timeSinceLastAttack += Time.deltaTime;
            if (timeSinceLastAttack >= underAttackCooldown)
            {
                radarDotAnimator.SetBool("underAttack", false);
            }
        }
        UpdateLvlOfPower();
    }

    /// <summary>
    /// "SwitchFase's" sole purpose is to activate a trigger in the "Animator" and to reset the "lvlOfPower".
    /// </summary>
    void SwitchFase()
    {
        buildingAnimator.SetTrigger("nextStageTrigger");
		lvlOfPower = 0;
    }

    /// <summary>
    /// This function is made to make it easier to update the power 
    /// after certain events in the duration of the session.
    /// </summary>
    void UpdateLvlOfPower()
    {
        buildingAnimator.SetFloat("amountOfPower", lvlOfPower / maxLvlOfPower);
    }

    /// <summary>
    /// This function is being used for three things: 
    /// first thing it does is deal damage to the building,
    /// second thing it does is set the "radarDot" bool on true 
    /// so that the "Animator" plays the "radarDot" animation
    /// and the last thing it does is invoke the "Unity event OnEmptyCharge"
    /// when there is no more damage to deal.
    /// </summary>
    /// <param name="value"></param>
    public void Damage(float value)
    {
        if (timeSinceLastAttack != 0)
            radarDotAnimator.SetBool("underAttack", true);

        timeSinceLastAttack = 0;

        lvlOfPower -= value;

        if (fullyHealed)
            fullyHealed = false;
            

        if (lvlOfPower < 0)
        {
            lvlOfPower = 0;
            OnEmptyCharge.Invoke();
        }
    }

    /// <summary>
    /// This does the exact opposite as the function "Damage"
    /// with an addition of starting an IEnumerator called "sfxPlayer"
    /// when it is completely charged.
    /// </summary>
    /// <param name="value"></param>
    public void Heal(float value)
    {
        if (UnderAttack)
            value *= 0.1f;
        if (lvlOfPower < maxLvlOfPower)
        {
            lvlOfPower += value;
        }
        else if (lvlOfPower >= maxLvlOfPower && !fullyHealed)
        {
            fullyHealed = true;
            StartCoroutine(sfxPlayer(8));
            OnFullCharge.Invoke();
            lvlOfPower = maxLvlOfPower;
        }
    }

    /// <summary>
    /// An IEnumerator that plays the soundeffects 
    /// that has an equal int called "whatSong" as the "soundManager".
    /// </summary>
    /// <param name="whatSong"></param>
    /// <returns></returns>
    IEnumerator sfxPlayer(int whatSong)
    {
        buildingSfx.clip = sM.SfxHolder[whatSong];
        buildingSfx.Play();
        yield return buildingSfx;
    }
}
