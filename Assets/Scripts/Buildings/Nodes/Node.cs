using UnityEngine;

/// <summary>
/// This script will act as a "Node" or "Target" 
/// and when it is hit it will "charge" the "Tower" or "Building" it's connected to.
/// </summary>
public class Node : MonoBehaviour, IDamagable
{
    // This variable is used so that this script knows 
    // that it has a building it can be attached to.
    public Building parentBuilding;

    // This variable works as a multiplier 
    // for the amount of power the laser sends.
    [SerializeField]
    private float energyBoost = 1;

    /// <summary>
    /// MaxHealth is necessary for the IDamagable class.
    /// </summary>
    public float MaxHealth
    {
        get
        {
            return parentBuilding.MaxHealth;
        }
    }

    /// <summary>
    /// Health is necessary for the IDamagable class.
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    public void OnHitStay()
    {
        parentBuilding.LvlOfPower = energyBoost;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void Damage(float value)
    {
        parentBuilding.Damage(value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void Heal(float value = 0)
    {
        if (value == 0)
            value = energyBoost;
        else
            value *= energyBoost;

        parentBuilding.Heal(value);
    }
}
