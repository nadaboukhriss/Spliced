using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    public ContactFilter2D movementFilter;

    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float collisionOffset = 0.001f;
    
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    
    Rigidbody2D rigidbody2d;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Animator animator;

    SwapCharacters swapCharacters;

    bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        swapCharacters = GetComponent<SwapCharacters>();
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
        }else{
            animator.SetBool("isWalking", false);
        }

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
        if (canMove)
        {
            animator.SetTrigger("swordAttack");
        }
        
    }

    public void LockMovement(){
        canMove = false;
    }

    public void UnlockMovement(){
        canMove = true;
    }
}
