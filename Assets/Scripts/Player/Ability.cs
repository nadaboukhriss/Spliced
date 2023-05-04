using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Ability : MonoBehaviour
{
    public float abilityCooldown = 0f;
    public Sprite abilityIcon;

    private float currentCooldown;

    private Player player;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player.GetComponent<Player>();
        playerController = GameManager.Instance.player.GetComponent<PlayerController>();
    }
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
        currentCooldown = abilityCooldown;
    }

    public float GetCurrentCooldown()
    {
        return currentCooldown;
    }
    public float GetMaxCooldown()
    {
        return abilityCooldown;
    }

    public float GetPercentageLeft()
    {
        if(abilityCooldown > 0)
        {
            return currentCooldown / abilityCooldown;
        }
        return 0;
        
    }
    public bool OnCooldown()
    {
        return currentCooldown > 0;
    }
}
