using UnityEngine;

/// <summary>
/// This script will act as a "Node" or "Target" and when it is hit it will "charge" the "Tower" or "Building" its connected to.
/// </summary>
public class Node : MonoBehaviour, IDamagable
{
    public Building parentBuilding;

    [SerializeField]
    private float energyBoost = 1;

    /// <summary>
    /// MaxHealth is nodig voor de IDamagable class
    /// </summary>
    public float MaxHealth
    {
        get
        {
            return parentBuilding.MaxHealth;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public float Health
    {
        get
        {
            return parentBuilding.Health;
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
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
