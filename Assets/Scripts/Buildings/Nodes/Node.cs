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
    private float _nodeMaxEnergy = 25;


    [SerializeField]
    private float _currentNodeEnergy = 0;


    [SerializeField]
    private float _energyBoost = 1;

    public int maxHealth
    {
        get
        {
            return (int)_nodeMaxEnergy;
        }
    }

    public int health
    {
        get
        {
            return parentBuilding.health;
        }
    }
    
    void Start()
    {
        parentBuilding = GetComponentInParent<Building>();
    }
    
    void Update()
    {

        if (Input.GetKey(KeyCode.T))
        {
            Heal((int)_energyBoost);
        }

        if (_currentNodeEnergy >= _nodeMaxEnergy)
        {
            Destroy(gameObject);
        }
        else if (_currentNodeEnergy <= _nodeMaxEnergy)
        {

        }
    }

    public void Damage(int value)
    {
        parentBuilding.Damage(value);
    }

    public void Heal(int value = 0)
    {
        if (value == 0)
            value = (int)_energyBoost;
        else
            value *= (int)_energyBoost;

        parentBuilding.Heal(value);
    }
}
