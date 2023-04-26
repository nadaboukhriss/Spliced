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

    Rigidbody2D rigidbody2d;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Collider2D movementCollider;
    PlayerInput inputActions;
    private int currentAvatar;

    SwapCharacters swapCharacters;

    public float dirRight;
    public float dirUp;

    [Header("Personalities")]
    public PersonalityController human;
    public PersonalityController fox;

    private PersonalityController currentShape;
    [Header("Shared Abilities")]
    public BaseAbility switchAbility;

    bool canMove = true;
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
        switchAbility.Init();
        human.Init();
        fox.Init();
        currentShape = human;
    }

    public void Update()
    {
        switchAbility.ReduceCooldown();
        human.ReduceCooldown();
        fox.ReduceCooldown();
    }
    private void FixedUpdate() {
        if(movementInput != Vector2.zero && canMove){
            animator.SetFloat("XInput", movementInput.x);
            animator.SetFloat("YInput", movementInput.y);
            bool success = TryMove(movementInput);
            if(!success && movementInput.x != 0)
            {
                success = TryMove(new Vector2(movementInput.x,0));
            }
            if(!success && movementInput.y != 0)
            {
                success = TryMove(new Vector2(0,movementInput.y));
            }
            animator.SetBool("isWalking", success);
            

            dirRight = movementInput.x != 0 ? Mathf.Sign(movementInput.x) : 0;
            dirUp = movementInput.y != 0 ? Mathf.Sign(movementInput.y) : 0;
            

        }else{
            animator.SetBool("isWalking", false);
        }
        
        pastPos = transform.position;
    }

    private bool TryMove(Vector2 direction){
        int count = movementCollider.Cast(
            direction,
            movementFilter,
            castCollisions,
            moveSpeed*Time.fixedDeltaTime + collisionOffset
        );

        if(count == 0){
            //Movement speed is determined based on the current shape of the player.
            rigidbody2d.MovePosition(rigidbody2d.position + direction*currentShape.moveSpeed*Time.fixedDeltaTime);
            return true;
        }
        return false;
    }
    public void OnMove(InputAction.CallbackContext ctx){
        movementInput = ctx.ReadValue<Vector2>();
    }

    public BaseAbility GetSwitchAbility()
    {
        return switchAbility;
    }

    public BaseAbility GetBasicAbility()
    {
        return currentShape.basicAbility;
    }

    public BaseAbility GetSpecialAbility()
    {
        return currentShape.specialAbility;
    }
    public void OnBasicAttack(InputAction.CallbackContext ctx){
        if (canMove && ctx.performed)
        {
            if (!currentShape.basicAbility.isOnCooldown)
            {
                currentShape.basicAbility.StartCooldown();
                currentShape.basicAbility.Use();
            }
        }
       
    }

    public void OnSpecialAttack(InputAction.CallbackContext ctx)
    {
        if (canMove && ctx.performed)
        {
            if (!currentShape.specialAbility.isOnCooldown)
            {
                currentShape.specialAbility.StartCooldown();
                currentShape.specialAbility.Use();
            }
        }
    }

    public void OnSwitchCharacter(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!switchAbility.isOnCooldown)
            {
                switchAbility.StartCooldown();
                switchAbility.Use();
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
