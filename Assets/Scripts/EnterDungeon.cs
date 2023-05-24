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
    private GameObject fadeInFadeOutElt;
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
                GameManager.Instance.fadeScreen.StartFade(teleportTo.position);
            }
            else
            {
                cantEnterDialogue.TriggerDialogue("Entrance", GetComponent<SpriteRenderer>().sprite);
            }
        }

    }

    IEnumerator ActivateAndTeleport()
    {
    // // GameObject prefab = Resources.Load<GameObject>("FadeInFadeOut");
        //fadeInFadeOutElt = Instantiate(prefab);
        //fadeInFadeOutElt.SetActive(true); // First line

        Animator animator = fadeInFadeOutElt.GetComponent<Animator>();
        animator.SetTrigger("FadeIn");

        yield return new WaitForSeconds(0.8f); // Wait for 1 second

        playerCollider.transform.position = teleportTo.position; // Second line

        yield return new WaitForSeconds(0.25f); // Wait for 1 second
        animator.SetTrigger("FadeOut"); // Third line

        // fadeInFadeOutElt.SetActive(false);
        //Destroy(fadeInFadeOutElt);
    }
}