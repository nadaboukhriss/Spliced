using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{   
    public Dialogue dialogue;
    // public GameObject currentPlayer;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private ItemObject itemToGive;
    [SerializeField]
    private ProvideItemTo provideItemTo = ProvideItemTo.None;


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

            dialogue.TriggerDialogue(objectName, objectSprite, isHuman);

            if(provideItemTo == ProvideItemTo.Human && isHuman){
                player.inventory.AddItem(itemToGive);
            }
            else if(provideItemTo == ProvideItemTo.Fox && !isHuman){
                player.inventory.AddItem(itemToGive);
            }
            else if(provideItemTo == ProvideItemTo.Both){
                player.inventory.AddItem(itemToGive);
            }
            else if(provideItemTo == ProvideItemTo.None){
                // do nothing
            }
        }
    }
}

public enum ProvideItemTo{
    Human,
    Fox,
    Both,
    None
}
