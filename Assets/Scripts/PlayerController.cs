using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public ContactFilter2D movementFilter;
    public GameObject fireballPrefab;
    //public float fireballSpeed = 10f;
    //public float damage = 5f;

    private Vector3 pastPos;
    private Vector3 difference;

    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float collisionOffset = 0.001f;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Collider2D movementCollider;

    public Rigidbody2D rigidbody2d;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Animator animator;
    
    PlayerInput inputActions;
    SwapCharacters swapCharacters;

    private int currentAvatar;
    private Vector2 lastMovementInput = Vector2.zero;
    

    public float dirRight;
    public float dirUp;

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
    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        swapCharacters = GetComponent<SwapCharacters>();
        movementCollider = GetComponent<Collider2D>();
        inputActions = GetComponent<PlayerInput>();
        pastPos = transform.position;   
    }

    private void Start()
    {
        currentShape = human;
    }

    public void Update()
    {
        switchAbility.ReduceCooldown();
        human.ReduceCooldown();
        fox.ReduceCooldown();
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
        /*if (rigidbody2d.IsTouchingLayers())
        {

            animator.SetBool("isWalking", false);
            rigidbody2d.velocity = Vector2.zero;
        }*/
        if (boosting) return;
        if(movementInput != Vector2.zero && canMove){
            animator.SetFloat("XInput", movementInput.x);
            animator.SetFloat("YInput", movementInput.y);
            rigidbody2d.velocity = movementInput.normalized * currentShape.moveSpeed;
            lastMovementInput = movementInput;
            animator.SetBool("isWalking", true);
            /*bool success = TryMove(movementInput);
            if(!success && movementInput.x != 0)
            {
                success = TryMove(new Vector2(movementInput.x,0));
            }
            if(!success && movementInput.y != 0)
            {
                success = TryMove(new Vector2(0,movementInput.y));
            }
            animator.SetBool("isWalking", success);*/


            dirRight = movementInput.x != 0 ? Mathf.Sign(movementInput.x) : 0;
            dirUp = movementInput.y != 0 ? Mathf.Sign(movementInput.y) : 0;
            

        }else{
            rigidbody2d.velocity = Vector2.zero;
            animator.SetBool("isWalking", false);
        }

        

        pastPos = transform.position;
    }

    private bool TryMove(Vector2 direction){
        /*int count = movementCollider.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed*Time.fixedDeltaTime + collisionOffset
        );

        if(count == 0){
            //Movement speed is determined based on the current shape of the player.
            rigidbody2d.MovePosition(rigidbody2d.position + direction*currentShape.moveSpeed*Time.fixedDeltaTime);
            return true;
        }*/
        
        
        
        return true;
    }
    public void OnMove(InputAction.CallbackContext ctx){
        movementInput = ctx.ReadValue<Vector2>();
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
                LockMovement();

                float attackDistance = 1f;
                if (currentShape == human){
                    // Find all enemy objects in range
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackDistance, LayerMask.GetMask("Enemy"));

                    // Deal damage to each enemy
                    foreach (Collider2D enemy in hitEnemies)
                    {
                        enemy.GetComponent<Enemy>().TakeDamage(5);
                    }
                }

                Vector3 mousePos = GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector3 target_direction = (mousePos - fireballSpawnPoint.position);
                Vector3 direction = new Vector2(target_direction.x, target_direction.y).normalized;
                animator.SetFloat("XAttack", direction.x);
                animator.SetFloat("YAttack", direction.y);
                animator.SetTrigger("basicAbility");

                
            }
        }
    }

    //My attempt to create a nice layout failed, seems that a better approach is doing it through the animator
    public void FireBallAttack(AnimationEvent evt)
    {
        if (launchedFireBall) return;

        launchedFireBall = true;
        UnlockMovement();
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
        fireballInstance.GetComponent<Fireball>().dirUp = dirUp;
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

    public void OnTriggerEnter2D(Collider2D other){
        // print("Hit!");
        // if(other.tag == "Enemy"){
        //     print("Hit enemy!");
            
        // }
    }
}
