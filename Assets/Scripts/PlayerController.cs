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

    public Rigidbody2D rigidbody2d;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Collider2D movementCollider;
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

        /*if (Input.GetKeyDown(KeyCode.LeftShift) && !boosting)
        {
            Debug.Log("boost");
            boosting = true;
            rigidbody2d.velocity = lastMovementInput.normalized * 15;
            Invoke(nameof(StopDash), 0.15f);
            //StartCoroutine(BoostMovementSpeedForDuration(0.15f));
        }
        */
    }

    public Vector2 GetLastOrientering()
    {
        return lastMovementInput;
    }
    public void SetVelocity(Vector2 vec)
    {
        rigidbody2d.velocity = vec;
    }

    private void SetFalse()
    {
        boosting = false;
    }
    public void SetBoosting(bool arg)
    {
        boosting = arg;
    }


    private void FixedUpdate() {
        if (rigidbody2d.IsTouchingLayers())
        {

            animator.SetBool("isWalking", false);
            rigidbody2d.velocity = Vector2.zero;
        }
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

        if (rigidbody2d.IsTouchingLayers())
        {
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
        if (canMove && ctx.performed)
        {
            if (currentShape.basicAbility.IsReady())
            {
                LockMovement();
                animator.SetTrigger("basicAbility");
                currentShape.basicAbility.Activate();
            }
        }
    }

    //My attempt to create a nice layout failed, seems that a better approach is doing it through the animator
    public void FireBallAttack()
    {
        UnlockMovement();
        GameObject fireballPrefab = Resources.Load<GameObject>("Fireball");
        GameObject fireballInstance = Instantiate(fireballPrefab);

        //Vector3 mousePos = Mouse.current.position.ReadValue();
        //Vector3 target_direction = (mousePos - fireballSpawnPoint.position).normalized;
        //Quaternion rotation = Quaternion.LookRotation(target_direction);

        Vector2 direction = transform.right;


        Vector3 pos = fireballSpawnPoint.position;
        fireballInstance.transform.position = pos;
        
        fireballInstance.GetComponent<Fireball>().travelDirection = lastMovementInput;
        fireballInstance.GetComponent<Fireball>().dirUp = dirUp;
    }

    public void DashAbility()
    {
        Debug.Log("Start dashing");
    }

    public void HealAbility()
    {
        Debug.Log("Fox heal ");
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
