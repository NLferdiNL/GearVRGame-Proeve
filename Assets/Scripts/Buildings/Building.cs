using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    // This script is used too keep track of the buildings level of power and animate accordingly.


    // _lvlOfPower is called this way because: it tracks the amount of "Power" the "building" has.
    [SerializeField]
    private float _lvlOfPower = 100;

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

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.color = Color.Lerp(startColour, andColour, _lvlOfPower / 100);
    }
}
