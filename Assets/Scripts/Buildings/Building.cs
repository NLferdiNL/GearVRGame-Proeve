using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour, IDamagable
{
    // This script is used too keep track of the buildings level of power and animate accordingly.

    [SerializeField]
    public Animator buildingAnimator, radarDotAnimator;

    // lvlOfPower is called this way because: it tracks the amount of "Power" the "building" has.
    [SerializeField]
    private float lvlOfPower = 0;

    // maxLvlOfPower is called this way because: we need to make sure it doesent go over a limit.
    [SerializeField]
    private float maxLvlOfPower = 100;

	[SerializeField]
	float underAttackCooldown = 2f;

	float timeSinceLastAttack = 0;

    public Color startColour;
    public Color andColour;

    // This is a Unity Event handeler
    [Serializable]
    public class BuildingFullyGharged : UnityEvent { }

    public BuildingFullyGharged OnFullCharge = new BuildingFullyGharged();

    [SerializeField]
    private bool multipleAnimations, StagedAnimations;


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

	public bool UnderAttack {
		get {
			return timeSinceLastAttack >= underAttackCooldown;
		}
	}

    // Use this for initialization
    void Start()
    {
        if (buildingAnimator == null)
        {
            buildingAnimator = GetComponentInParent<Animator>();
        }
    }

    void FixedUpdate()
    {
		if(timeSinceLastAttack < underAttackCooldown) {
			timeSinceLastAttack += Time.deltaTime;
			if(timeSinceLastAttack >= underAttackCooldown) {
				radarDotAnimator.SetBool("underAttack", false);
			}
		}
			
		//no
        //PowerEqualsColour();

        if (maxLvlOfPower <= 100 && !multipleAnimations)
        {
            buildingAnimator.SetFloat("amountOfPower", lvlOfPower / maxLvlOfPower);
        }

        if (lvlOfPower >= maxLvlOfPower )
        {
            SwitchFase();
        }
    }

    // Temp
    public void DebugUpdate(string buildingName)
    {
        Debug.Log("Yay " + buildingName + " is fully charged");
    }

    private void PowerEqualsColour()
    {
        GetComponent<Renderer>().material.color = Color.Lerp(startColour, andColour, 100 / maxLvlOfPower * lvlOfPower / 100);
    }

    void SwitchFase()
    {
        if (maxLvlOfPower == 100)
        {
            maxLvlOfPower += 100;
        }
        OnFullCharge.Invoke();
    }

    public void Damage(float value)
    {
		if(timeSinceLastAttack != 0)
			radarDotAnimator.SetBool("underAttack", true);

		timeSinceLastAttack = 0;

        lvlOfPower -= value;

		if(lvlOfPower < 0)
			lvlOfPower = 0;
    }

    public void Heal(float value)
    {
        lvlOfPower += value;

		if(lvlOfPower > maxLvlOfPower)
			lvlOfPower = maxLvlOfPower;
    }
}
