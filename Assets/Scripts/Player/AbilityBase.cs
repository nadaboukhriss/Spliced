using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityBase
{
    public Sprite abilityIcon;
    [SerializeField]
    private float cooldown;

    private bool isOnCooldown = false;
    private float remainingCooldown = 0f;

    // Update is called once per frame
    public void ReduceCooldown()
    {
        if (isOnCooldown)
        {
            remainingCooldown -= Time.deltaTime;
            if (remainingCooldown <= 0)
            {
                Debug.Log("Ready!");
                remainingCooldown = 0;
                isOnCooldown = false;
            }
        }
    }
    public void Activate()
    {
        remainingCooldown = cooldown;
        isOnCooldown = true;
    }

    public float GetCooldownFraction()
    {
        if (cooldown > 0)
        {
            return remainingCooldown / cooldown;
        }
        return 0;

    }

    public bool IsReady()
    {
        return !isOnCooldown;
    }
}
