using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This Script basicly holds the current lvl of Power for 
/// Influences the animatior normalizedTime based on the AudioData.GetFloat(range) height * amplify.
/// To create a smoother animation it uses MoveTowards with barSpeed so the speed can be edited in
/// the inspector.
/// </summary>
public class Building : MonoBehaviour, IDamagable
{
    // 
    [SerializeField]
    public Animator buildingAnimator, radarDotAnimator;

    // 
    [SerializeField]
    private float lvlOfPower = 0;

    // 
    [SerializeField]
    private float maxLvlOfPower = 100;
    
    //
    [SerializeField]
    float underAttackCooldown = 2f;
    
    //
	[SerializeField]
    float timeSinceLastAttack = 0;
	
    //
    [Serializable]
    public class BuildingFullyChargedEvent : UnityEvent { }

    public BuildingFullyChargedEvent OnFullCharge = new BuildingFullyChargedEvent();


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

    public bool UnderAttack
    {
        get
        {
            return timeSinceLastAttack < underAttackCooldown;
        }
    }

    void Start()
    {
        if (buildingAnimator == null)
        {
            buildingAnimator = GetComponentInParent<Animator>();
        }
        SoundController.Instance.OnReset.AddListener(SwitchFase);
    }

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

    void SwitchFase()
    {
        //UpdateLvlOfPower();
        buildingAnimator.SetTrigger("nextStageTrigger");
		lvlOfPower = 0;
    }

    void UpdateLvlOfPower()
    {
        buildingAnimator.SetFloat("amountOfPower", lvlOfPower / maxLvlOfPower);
    }

    public void Damage(float value)
    {

        if (timeSinceLastAttack != 0)
            radarDotAnimator.SetBool("underAttack", true);

        timeSinceLastAttack = 0;

        lvlOfPower -= value;

        if (fullyHealed)
            fullyHealed = false;

        if (lvlOfPower < 0)
            lvlOfPower = 0;
    }

    public void Heal(float value)
    {
        if (UnderAttack)
            value *= 0.1f;
        if (lvlOfPower < maxLvlOfPower)
        {
            lvlOfPower += value;
        }

        if (lvlOfPower > maxLvlOfPower && !fullyHealed)
        {
            fullyHealed = true;
            OnFullCharge.Invoke();
            lvlOfPower = maxLvlOfPower;
        }
    }
}
