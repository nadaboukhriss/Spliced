using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public ContactFilter2D movementFilter;
    public GameObject fireballPrefab;

    private Vector3 pastPos;
    private Vector3 difference;

    //[SerializeField] float collisionOffset = 0.001f;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Collider2D movementCollider;

    public Rigidbody2D rigidbody2d;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Player player;
    SwapCharacters swapCharacters;

    private int currentAvatar;
    private Vector2 lastMovementInput = Vector2.zero;

    [Header("Personalities")]
    public PersonalityController2 human;
    public PersonalityController2 fox;
    [SerializeField]
    private Transform fireballSpawnPoint;

    private PersonalityController2 currentShape;
    [Header("Shared Abilities")]
    [SerializeField]
    public AbilityBase switchAbility;

    bool canMove = true;

    bool boosting = false;
    // Start is called before the first frame update

    private bool launchedFireBall = false;

    private List<SinkingTile> standingOnTiles;
    private bool standingOnLava = false;
    private float lavaDamageTickSpeed = 1.0f;
    private float tickCooldown = 0f;
    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        swapCharacters = GetComponent<SwapCharacters>();
        movementCollider = GetComponent<Collider2D>();
        //inputActions = GetComponent<PlayerInput>();
        pastPos = transform.position;   
    }

    private void Start()
    {
        currentShape = human;
        standingOnTiles = new List<SinkingTile>();
        player = GetComponent<Player>();
    }

    public void Update()
    {
        switchAbility.ReduceCooldown();
        human.ReduceCooldown();
        fox.ReduceCooldown();

        tickCooldown -= Time.deltaTime;
        if (standingOnLava && tickCooldown <= 0)
        {
            tickCooldown = lavaDamageTickSpeed;
            foreach (SinkingTile tile in standingOnTiles)
            {
                if (!tile.submerged)
                {
                    return;
                }
            }
            //Not safe on tile;
            player.TakeDamage(10);
        }
    }

    public Vector2 GetLastOrientering()
    {
        return lastMovementInput;
    }
    public void SetVelocity(Vector2 vec)
    {
        rigidbody2d.velocity = vec;
    }
    public void SetBoosting(bool arg)
    {
        boosting = arg;
    }

    public void LateUpdate()
    {
        launchedFireBall = false;
    }

    private void FixedUpdate() {
        if (boosting) return;
        if(movementInput != Vector2.zero && canMove){
            animator.SetFloat("XInput", movementInput.x);
            animator.SetFloat("YInput", movementInput.y);

            bool abnormalSpeed = rigidbody2d.velocity.magnitude > currentShape.moveSpeed;
            rigidbody2d.AddForce(movementInput.normalized * currentShape.moveSpeed * 1000 * Time.deltaTime);

            if(rigidbody2d.velocity.magnitude > currentShape.moveSpeed && !abnormalSpeed)
                rigidbody2d.velocity = Vector2.ClampMagnitude(rigidbody2d.velocity, currentShape.moveSpeed);
            
            lastMovementInput = movementInput;
            animator.SetBool("isWalking", true);

        }else{
            animator.SetBool("isWalking", false);
        }
        pastPos = transform.position;
    }

    public PersonalityController2 GetCurrentShape()
    {
        return currentShape;
    }

    public bool isHuman(){
        return (currentShape == human);
    }

    public void OnMove(InputAction.CallbackContext ctx){
        movementInput = ctx.ReadValue<Vector2>();
    }

    public bool IsPlayingAelia()
    {
        return swapCharacters.getCurrentCharacter() == 1;
    }

    public bool IsPlayingAsh()
    {
        return swapCharacters.getCurrentCharacter() == 2;
    }

    public AbilityBase GetSwitchAbility()
    {
        return switchAbility;
    }

    public AbilityBase GetBasicAbility()
    {
        return currentShape.basicAbility;
    }

    public AbilityBase GetSpecialAbility()
    {
        return currentShape.specialAbility;
    }
    public void OnBasicAttack(InputAction.CallbackContext ctx){
        if (canMove && ctx.started)
        {
            if (currentShape.basicAbility.IsReady())
            {

                currentShape.basicAbility.Activate();
                Vector3 mousePos = GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector3 target_direction = (mousePos - fireballSpawnPoint.position);
                Vector3 direction = new Vector2(target_direction.x, target_direction.y).normalized;
                animator.SetFloat("XAttack", direction.x);
                animator.SetFloat("YAttack", direction.y);
                animator.SetFloat("XInput", direction.x);
                animator.SetFloat("YInput", direction.y);
                animator.SetTrigger("basicAbility");

                
            }
        }
    }

    //My attempt to create a nice layout failed, seems that a better approach is doing it through the animator
    public void FireBallAttack(AnimationEvent evt)
    {
        if (launchedFireBall) return;

        launchedFireBall = true;
        GameObject fireballPrefab = Resources.Load<GameObject>("Fireball");
        GameObject fireballInstance = Instantiate(fireballPrefab);

        Vector3 mousePos = GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 target_direction = new Vector2(animator.GetFloat("XAttack"),animator.GetFloat("YAttack"));// (mousePos - fireballSpawnPoint.position).normalized;
        float rotZ = Mathf.Atan2(target_direction.y, target_direction.x) * Mathf.Rad2Deg;

        Vector2 direction = transform.right;


        Vector3 pos = fireballSpawnPoint.position;
        fireballInstance.transform.position = pos;
        fireballInstance.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        
        fireballInstance.GetComponent<Fireball>().travelDirection = target_direction;
        fireballInstance.GetComponent<Fireball>().SetDamage(currentShape.GetBasicDamage());
        fireballInstance.GetComponent<Fireball>().SetKnockbackForce(currentShape.GetKnockbackForce());

        //SFX
        SFXManager.sfxinstance.Audio.PlayOneShot(SFXManager.sfxinstance.FireballSound);
    }

    public void DashAbility()
    {
        Debug.Log("Start dashing");
    }

    public void HealAbility()
    {
        
    }

    public void OnSpecialAttack(InputAction.CallbackContext ctx)
    {
        if (canMove && ctx.performed)
        {
            if (currentShape.specialAbility.IsReady())
            {
                currentShape.SpecialAbility();
            }
        }
    }

    public void OnSwitchCharacter(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (switchAbility.IsReady())
            {
                switchAbility.Activate();
                swapCharacters.SwapCharacter();
            }
        }

    }

    public void SwitchAbilities()
    {
        if (swapCharacters.getCurrentCharacter() == 2)
        {
            //Switch back to human
            currentShape = human;
        }
        else
        {
            currentShape = fox;
        }
    }
    public void LockMovement(){
        canMove = false;
    }

    public void UnlockMovement(){
        canMove = true;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {

        SinkingTile tile = collision.GetComponent<SinkingTile>();
        if(tile)
        {
            standingOnTiles.Add(tile);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Lava")) standingOnLava = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        SinkingTile tile = collision.GetComponent<SinkingTile>();
        if (tile)
        {
            int remove = -1;
            for(int i = 0; i < standingOnTiles.Count; i++)
            {
                if (standingOnTiles[i] == tile)
                {
                    remove = i;
                    break;
                }
            }
            if (remove != -1) standingOnTiles.RemoveAt(remove);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Lava")) standingOnLava = false;
    }
}
