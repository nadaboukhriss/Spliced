using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingTile : MonoBehaviour
{

    [SerializeField]
    private float timeBetweenSteps;
    [SerializeField]
    private float sinkStep = 0.1f;
    [SerializeField]
    private bool interactWithEnemies = true;
    [SerializeField]
    private int sinkingLoad = 1;

    public bool submerged = false;

    private SpriteRenderer spriteRenderer;
    private int numObjectsOnTile = 0;
    private float stepCooldown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        stepCooldown = timeBetweenSteps;
        spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        stepCooldown -= Time.deltaTime;
        if (stepCooldown < 0) stepCooldown = 0;
        if(stepCooldown <= 0)
        {
            stepCooldown = timeBetweenSteps;
            Color c = spriteRenderer.color;
            Debug.Log("Num object on tile: " + numObjectsOnTile);
            //If more than load limit is on tile, sink it, otherwise raise it.
            if (numObjectsOnTile >= sinkingLoad)
            {
                c.a -= sinkStep;
                if (c.a < 0) c.a = 0;
            }
            else
            {
                c.a += sinkStep;
                if (c.a > 1) c.a = 1;
            }
            spriteRenderer.color = c;
            if (c.a <= 0) submerged = true;
            else submerged = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        Enemy enemy = collision.GetComponent<Enemy>();
        if (player || (interactWithEnemies && enemy))
        {
            numObjectsOnTile += 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        Enemy enemy = collision.GetComponent<Enemy>();
        if (player || (interactWithEnemies && enemy))
        {
            numObjectsOnTile -= 1;
            if (numObjectsOnTile < 0) numObjectsOnTile = 0;
        }
    }
}
