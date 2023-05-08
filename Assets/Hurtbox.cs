using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        print("enter");
        Enemy enemy = collision.GetComponentInParent<Enemy>();
        if (enemy)
        {
            player.TakeDamage((int)enemy.GetDamage());
        }
    }*/
}
