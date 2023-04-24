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

    [Header("Abilities")]
    public BaseAbility switchAbility;
    public List<BaseAbility> humanAbilities;
    public List<BaseAbility> foxAbilities;
    private List<BaseAbility> currentAbilities;

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

        switchAbility.Init();
        foreach (BaseAbility ability in humanAbilities)
        {
            ability.Init();
        }
        foreach (BaseAbility ability in foxAbilities)
        {
            ability.Init();
        }
        currentAbilities = humanAbilities;
    }

    public void Update()
    {
        switchAbility.ReduceCooldown();
        foreach (BaseAbility ability in humanAbilities)
        {
            ability.ReduceCooldown();
        }
        foreach (BaseAbility ability in foxAbilities)
        {
            ability.ReduceCooldown();
        }
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
            rigidbody2d.MovePosition(rigidbody2d.position + direction*moveSpeed*Time.fixedDeltaTime);
            return true;
        }
        return false;
    }
    public void OnMove(InputAction.CallbackContext ctx){
        movementInput = ctx.ReadValue<Vector2>();
    }

    public List<BaseAbility> GetCurrentAbilities()
    {
        return currentAbilities;
    }

    public BaseAbility GetSwitchAbility()
    {
        return switchAbility;
    }
    public void OnBasicAttack(InputAction.CallbackContext ctx){
        if (canMove && ctx.performed)
        {
            currentAvatar = swapCharacters.getCurrentCharacter();

            if (!currentAbilities[0].isOnCooldown)
            {
                currentAbilities[0].StartCooldown();
                currentAbilities[0].Use();
            }
            
            /*switch (currentAvatar) {
                case 1:
                    animator.SetTrigger("swordAttack");
                    LayerMask enemyLayers = LayerMask.GetMask("Enemy");
                    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 2f, enemyLayers);
                    foreach (Collider2D ene in hitEnemies) {
                        print("Hello there enemy!");
                        GameObject enemyGameObject = ene.gameObject;
                        Enemy enemyComponent = enemyGameObject.GetComponent<Enemy>();
                        
                        if(enemyComponent != null){
                            enemyComponent.TakeDamage(damage);
                        }
                    }
                    break;
                case 2:
                    animator.SetTrigger("swordAttack");
                    ThrowFireball();
                    break;
            }*/
        }
       
    }

    public void OnSpecialAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!currentAbilities[1].isOnCooldown)
            {
                currentAbilities[1].StartCooldown();
                currentAbilities[1].Use();
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
            currentAbilities = humanAbilities;
        }
        else
        {
            currentAbilities = foxAbilities;
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
