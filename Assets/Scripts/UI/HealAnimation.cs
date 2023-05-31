using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAnimation : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    public void Start()
    {
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("Heal");
    }
    public void Heal()
    {
        GameManager.Instance.player.GetComponent<Player>().Heal(5);
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
