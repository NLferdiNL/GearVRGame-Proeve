using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IDamagable
{
    // This script is used too keep track of the buildings level of power and animate accordingly.

    [SerializeField]
    public Animator buildingAnimator;

    // _lvlOfPower is called this way because: it tracks the amount of "Power" the "building" has.
    private float _lvlOfPower = 100;

    [SerializeField]
    private float _maxLvlOfPower = 100;

    public Color startColour;
    public Color andColour;

    public float LvlOfPower
    {
        get
        {
            return _lvlOfPower;
        }
        set
        {
            _lvlOfPower += value;
        }
    }
    
    public int maxHealth
    {
        get
        {
            return (int)_maxLvlOfPower;
        }
    }

    public int health
    {
        get
        {
            return (int)_lvlOfPower;
        }
    }

    // Use this for initialization
    void Start()
    {
        _lvlOfPower = _maxLvlOfPower;
        if (buildingAnimator == null)
        {
            buildingAnimator = GetComponentInParent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.color = Color.Lerp(startColour, andColour, 100 / _maxLvlOfPower * _lvlOfPower / 100);
        buildingAnimator.SetFloat("amountOfPower", _lvlOfPower);
    }

    public void Damage(int value)
    {
        _lvlOfPower -= value;
    }

    public void Heal(int value)
    {
        _lvlOfPower += value;
    }
}
