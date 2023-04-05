using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHealth = 10f;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float collisionOffset = 0.001f;
    [SerializeField] Collider2D collisionBox;

    [SerializeField] GameObject healthbar;
    public ContactFilter2D movementFilter;
    

    private float health;

    private Rigidbody2D rigidbody2d;
    private Vector2 movementInput;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        health = maxHealth;
    }
    private void Update(){

    }
    private void FixedUpdate() {
        if(movementInput != Vector2.zero && canMove){
            bool success = TryMove(movementInput);
            if(!success){
                success = TryMove(new Vector2(movementInput.x,0));
            }
            if(!success){
                success = TryMove(new Vector2(0,movementInput.y));
            }
            //animator.SetBool("isMoving",success);
            
            
        }else{
            //animator.SetBool("isMoving",false);
        }

        if(movementInput.x < 0){
            spriteRenderer.flipX = true;
        }else if(movementInput.x > 0){
            spriteRenderer.flipX = false;
        }

    }

    public void SetVelocity(Vector2 movementValue){
        movementInput = movementValue; 
    }

    public void Attack(){
        print("Attack!");
    }

    private bool TryMove(Vector2 direction){
        int count = collisionBox.Cast(
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

    public void TakeDamage(float damage){
        if(health >= 0){
            health -= damage;
            healthbar.transform.localScale = new Vector3(health/maxHealth,healthbar.transform.localScale.y,healthbar.transform.localScale.z);
            if(health <= 0){
                animator.SetTrigger("Dead");
            }
        }
        
    }

    public void Defeated(){
        Destroy(gameObject);
    }
    public void LockMovement(){
        //canMove = false;
    }

    public void UnlockMovement(){
        //canMove = true;
    }

}
