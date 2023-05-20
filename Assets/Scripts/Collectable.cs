using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private ItemObject item;

    public void Collect(Player player)
    {
        Debug.Log("Pick up item " + item.itemName);
        item.Collect(player);
        Destroy(gameObject);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player)
        {
            Collect(player);
        }
    }
}

