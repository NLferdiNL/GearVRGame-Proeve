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
    // This is needed for the animator
    [SerializeField]
    private float maxLvlOfPower = 100;
    
    // 
    [SerializeField]
    float underAttackCooldown = 2f;
    
    //
	[SerializeField]
    float timeSinceLastAttack = 0;
	
    //
    [SerializeField]
    private SoundManager sM;

    //
    private AudioSource buildingSfx;

    //
    [Serializable]
    public class BuildingFullyChargedEvent : UnityEvent { }

    public BuildingFullyChargedEvent OnFullCharge = new BuildingFullyChargedEvent();

<<<<<<< HEAD
	[Serializable]
	public class BuildingEmptyChargedEvent : UnityEvent { }
=======
    [Serializable]
    public class BuildingEmptyChargedEvent : UnityEvent { }

    public BuildingEmptyChargedEvent OnEmptyCharge = new BuildingEmptyChargedEvent();
>>>>>>> 1cac40a63f5388ec2f0185af47f26ddc57d4246d

	public BuildingEmptyChargedEvent OnEmptyCharge = new BuildingEmptyChargedEvent();

	bool fullyHealed = false;

    /// <summary>
    /// 
    /// </summary>
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
    /// <summary>
    /// 
    /// </summary>
    public float MaxHealth
    {
        get
        {
            return (int)maxLvlOfPower;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public float Health
    {
        get
        {
            return (int)lvlOfPower;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public bool UnderAttack
    {
        get
        {
            return timeSinceLastAttack < underAttackCooldown;
        }
    }
    /// <summary>
    /// 
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
    /// 
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
    /// 
    /// </summary>
    void SwitchFase()
    {
        //UpdateLvlOfPower();
        buildingAnimator.SetTrigger("nextStageTrigger");
		lvlOfPower = 0;
    }
    /// <summary>
    /// 
    /// </summary>
    void UpdateLvlOfPower()
    {
        buildingAnimator.SetFloat("amountOfPower", lvlOfPower / maxLvlOfPower);
    }
    /// <summary>
    /// 
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
            

<<<<<<< HEAD
		if(lvlOfPower <= 0) {
			lvlOfPower = 0;
			OnEmptyCharge.Invoke();
		}
=======
        if (lvlOfPower < 0)
        {
            lvlOfPower = 0;
            OnEmptyCharge.Invoke();
        }
>>>>>>> 1cac40a63f5388ec2f0185af47f26ddc57d4246d
    }
    /// <summary>
    /// 
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

    IEnumerator sfxPlayer(int whatSong)
    {
        buildingSfx.clip = sM.SfxHolder[whatSong];
        buildingSfx.Play();
        yield return buildingSfx;
    }
}