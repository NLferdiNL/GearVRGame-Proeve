using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
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

    // Use this for initialization
    void Start()
    {
        parentBuilding = GetComponentInParent<Building>();
    }

    public void OnHitStay()
    {
        parentBuilding.LvlOfPower = _energyBoost;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.T))
        {
            OnHitStay();
        }

        if (_currentNodeEnergy >= _nodeMaxEnergy)
        {
            Destroy(gameObject);
        }
        else if (_currentNodeEnergy <= _nodeMaxEnergy)
        {

        }
    }
}
