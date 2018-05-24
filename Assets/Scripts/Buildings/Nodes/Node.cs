using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour, IDamagable
{
    // This script will act as a "Node" or "Target" and when it is hit it will "charge" the "Tower" or "Building" its connected to.

    public Building parentBuilding;

    // _nodeMaxEnergy is called this way because: it tracks the amount of "Power" it passes to the "building".
    [SerializeField]
    private float nodeMaxEnergy = 25;


    [SerializeField]
    private float currentNodeEnergy = 0;


    [SerializeField]
    private float energyBoost = 1;

    public float MaxHealth
    {
        get
        {
            return (int)nodeMaxEnergy;
        }
    }

    public float Health
    {
        get
        {
            return parentBuilding.Health;
        }
    }
    
    void Start()
    {
		if(parentBuilding == null)
	        parentBuilding = GetComponentInParent<Building>();
    }

    public void OnHitStay()
    {
        parentBuilding.LvlOfPower = energyBoost;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.T))
        {
            Heal((int)energyBoost);
        }

        if (currentNodeEnergy >= nodeMaxEnergy)
        {
            Destroy(gameObject);
        }
        else if (currentNodeEnergy <= nodeMaxEnergy)
        {

        }
    }

    public void Damage(float value)
    {
        parentBuilding.Damage(value);
    }

    public void Heal(float value = 0)
    {
        if (value == 0)
            value = energyBoost;
        else
            value *= energyBoost;

        parentBuilding.Heal(value);
    }
}
