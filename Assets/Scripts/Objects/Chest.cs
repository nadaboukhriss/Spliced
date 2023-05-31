using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private float interactionDistance = 5f;
    [SerializeField]
    private Sprite openSprite;
    [SerializeField]
    private Sprite closedSprite;
    [SerializeField]
    private ItemObject itemToGive;
    [SerializeField]
    private bool useEmptyDialogue = false;
    [SerializeField]
    private Dialogue emptyDialogue;

    private bool opened = false;

    private Player playerInProximity = null;

    private Player player;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {

        GameManager.Instance.player.GetComponent<PlayerInput>().actions.FindAction("Interact").performed += InteractWithChest;
        player = GameManager.Instance.player.GetComponent<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, distance);

        if(hit)
        {
            Player testingForPlayer = hit.collider.GetComponent<Player>();
            if (testingForPlayer != null && distance <= interactionDistance)
            {
                playerInProximity = player;
            }
            else
            {
                playerInProximity = null;
            }
        }
        
    }
    private void InteractWithChest(InputAction.CallbackContext ctx)
    {
        if (playerInProximity != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray,Mathf.Infinity);
            if (hit)
            {
                if (!opened)
                {
                    opened = true;
                    spriteRenderer.sprite = openSprite;
                    itemToGive.Collect(playerInProximity);
                }
                else if (useEmptyDialogue && opened)
                {
                    emptyDialogue.TriggerDialogue("Empty", null);
                }
            }
            
            
        }
    }
}
