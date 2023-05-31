using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    // Update is called once per frame
    void Update()
    {
        
    }

    override
    protected void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
