using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{   
    public Dialogue dialogue;
    // public GameObject currentPlayer;
    private SpriteRenderer spriteRenderer;


     public void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        PlayerController controller = player.GetComponent<PlayerController>();
        if (player)
        {

            bool isHuman = controller.isHuman();
            string objectName = gameObject.name; 
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            Sprite objectSprite = spriteRenderer.sprite; 

            Debug.Log("Met with an NPC");

            SFXManager.sfxinstance.Audio.PlayOneShot(SFXManager.sfxinstance.PickupSound);
            dialogue.TriggerDialogue(objectName, objectSprite, isHuman);
        }
    }
}
