using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : ScriptableObject
{
    public string abilityName;
    public Sprite abilityIcon;
    public float cooldownTime;
    public bool isOnCooldown;

    protected float currentCooldown;
    protected PlayerController playerController;
    protected Player player;
    // Start is called before the first frame update
    public void Init()
    {
        player = GameManager.Instance.player.GetComponent<Player>();
        playerController = GameManager.Instance.player.GetComponent<PlayerController>();
        isOnCooldown = false;
        currentCooldown = 0;
    }
    public virtual void StartCooldown()
    {
        currentCooldown = cooldownTime;
        isOnCooldown = true;
    }

    public abstract void Use();
    public virtual void ReduceCooldown()
    {
        
        if (isOnCooldown)
        {
            currentCooldown -= Time.deltaTime;
            if(currentCooldown <= 0)
            {
                Debug.Log("Ready!");
                currentCooldown = 0;
                isOnCooldown = false;
            }
        }
    }

    public float GetPercentageLeft()
    {
        if (cooldownTime > 0)
        {
            return currentCooldown / cooldownTime;
        }
        return 0;

    }
}
