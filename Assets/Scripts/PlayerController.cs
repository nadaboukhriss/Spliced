using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    public ContactFilter2D movementFilter;
    public GameObject fireballPrefab;
    public float fireballSpeed = 10f;

    private Vector3 pastPos;
    private Vector3 difference;

    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float collisionOffset = 0.001f;
    
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    
    Rigidbody2D rigidbody2d;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Animator animator;
    private int currentAvatar;

    SwapCharacters swapCharacters;

    public float dirRight;
    public float dirUp;

    bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        swapCharacters = GetComponent<SwapCharacters>();
        pastPos = transform.position;
    }

    private void FixedUpdate() {
        if(movementInput != Vector2.zero && canMove){
            animator.SetFloat("XInput", movementInput.x);
            animator.SetFloat("YInput", movementInput.y);
            bool success = TryMove(movementInput);
            if(!success){
                success = TryMove(new Vector2(movementInput.x,0));
            }
            if(!success){
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
        int count = rigidbody2d.Cast(
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
    void OnMove(InputValue movementValue){
        
        movementInput = movementValue.Get<Vector2>();
        
        
    }

    void OnFire(){
        currentAvatar = swapCharacters.getCurrentCharacter();
        switch (currentAvatar) {
            case 1:
                animator.SetTrigger("swordAttack");
                break;
            case 2:
                animator.SetTrigger("swordAttack");
                ThrowFireball();
                break;
        }
       
    }

    void ThrowFireball(){
        GameObject fireballPrefab = Resources.Load<GameObject>("Fireball");
        GameObject fireballInstance = Instantiate(fireballPrefab);
        // Get the direction that the player is facing
        Vector2 direction = transform.right;
        
        fireballInstance.transform.position = transform.position;
        fireballInstance.GetComponent<Fireball>().dirRight = dirRight; 
        fireballInstance.GetComponent<Fireball>().dirUp = dirUp; 
    }


    public void LockMovement(){
        canMove = false;
    }

    public void UnlockMovement(){
        canMove = true;
    }

    public void OnTriggerEnter2D(Collider2D other){
        print("Hit!");
        if(other.tag == "Enemy"){
            print("Hit enemy!");
            
        }
    }
}
