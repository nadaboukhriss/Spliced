using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ability
{
    [SerializeField]
    private float maxCooldown = 0f;

    private float currentCooldown;
    private float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float GetDamage()
    {
        return damage;
    }
    // Update is called once per frame
    public void DecreaseCooldown()
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown < 0f)
            {
                currentCooldown = 0f;
            }
        }
    }

    public void SetCooldown()
    {
        currentCooldown = maxCooldown;
    }

    public float GetCurrentCooldown()
    {
        return currentCooldown;
    }
    public float GetMaxCooldown()
    {
        return maxCooldown;
    }

    public float GetPercentageLeft()
    {
        if(maxCooldown > 0)
        {
            return currentCooldown / maxCooldown;
        }
        return 0;
        
    }
    public bool OnCooldown()
    {
        return currentCooldown > 0;
    }
}
