using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EnterDungeon : MonoBehaviour
{
    //Index found in main menu: File > Build Settings > Scenes in Build
    public int switchToSceneWithBuildIndex;

    [SerializeField]
    private Transform teleportTo;
    [SerializeField]
    private bool requiresObjectToOpen = false;
    [SerializeField]
    private ItemType requirement;
    [SerializeField]
    private Dialogue cantEnterDialogue;

    private bool isPlayerInCollider = false;
    private Collider2D playerCollider;

    public void Start()
    {
        GameManager.Instance.player.GetComponent<PlayerInput>().actions.FindAction("Interact").performed += InteractWithDoor;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.tag == "Player")
        {
            isPlayerInCollider = true;
            playerCollider = other;
        } 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isPlayerInCollider = false;
    }   

    private bool CanEnter()
    {
        Player player = GameManager.Instance.player.GetComponent<Player>();
        if (requiresObjectToOpen && !player.inventory.HasItem(requirement)) return false;

        return true;
    }
    
    private void InteractWithDoor(InputAction.CallbackContext ctx)
    {
        if (isPlayerInCollider)
        {
            if (CanEnter())
            {
                playerCollider.transform.position = teleportTo.position;
            }
            else
            {
                cantEnterDialogue.TriggerDialogue("Entrance", GetComponent<SpriteRenderer>().sprite);
            }

        }
    }
}