using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IDamagable
{
    // This script is used too keep track of the buildings level of power and animate accordingly.

    [SerializeField]
    public Animator buildingAnimator;

    // lvlOfPower is called this way because: it tracks the amount of "Power" the "building" has.
    [SerializeField]
    private float lvlOfPower = 0;

    // maxLvlOfPower is called this way because: we need to make sure it doesent go over a limit.
    [SerializeField]
    private float maxLvlOfPower = 100;

    public Color startColour;
    public Color andColour;

    // This needs a Unity Event handeler

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

    // Use this for initialization
    void Start()
    {
        if (buildingAnimator == null)
        {
            buildingAnimator = GetComponentInParent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.color = Color.Lerp(startColour, andColour, 100 / maxLvlOfPower * lvlOfPower / 100);
        buildingAnimator.SetFloat("amountOfPower", lvlOfPower / 100);
    }

    public void Damage(float value)
    {
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
