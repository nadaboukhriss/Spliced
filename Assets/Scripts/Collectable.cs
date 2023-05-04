using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private ItemObject item;
    [SerializeField]
    private Dialogue dialogue;

    public void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player)
        {
            Debug.Log("Pick up item " + item.itemName);
            //SFXManager.sfxinstance.Audio.PlayOneShot(SFXManager.sfxinstance.PickupSound);
            dialogue.TriggerDialogue(item.itemName, item.icon);
            player.inventory.AddItem(item);
            Destroy(gameObject);
        }
    }
}

