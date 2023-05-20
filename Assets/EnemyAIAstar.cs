using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAIAstar : MonoBehaviour
{
    [SerializeField]
    protected Transform target;
    [SerializeField]
    protected LayerMask targetMask;

    [SerializeField]
    protected LayerMask obstacleMask;
    [SerializeField]
    protected float speed = 2f;
    [SerializeField]
    protected float nextWaypointDistance = 1f;
    [SerializeField]
    protected float detectionRange = 5f;
    [SerializeField]
    protected float attackRange = 3f;
    [SerializeField]
    protected Vector3 rangeOffset = Vector3.zero;
    [SerializeField]
    protected float rememberTargetTime = 1f; // How many seconds to remember the target after it can no longer be seen.
    [SerializeField]
    protected float attackCooldown = 1f;

    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Collider2D movementCollider;
    [SerializeField] float collisionOffset = 0.001f;


    [SerializeField]
    protected bool showGizmos = true;

    protected Path path;
    protected int currentWaypoint = 0;
    protected bool reachedEndOfPath = false;

    protected Vector2 startPosition;
    protected bool returnToStart = false;
    protected bool keepingTrackOfTarget = false;

    protected Seeker seeker;
    protected Rigidbody2D rigidbody2d;
    protected Animator animator;

    protected EnemyState state;

    protected float currentCooldown = 0f;

    protected bool isAlive = true;
    protected Enemy enemy;

    protected bool canMove = true;
    protected float lastSeenTarget = Mathf.NegativeInfinity;
    protected Vector2 movementDirection = Vector2.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        state = EnemyState.Idle;
        animator = GetComponent<Animator>();
        seeker = GetComponent<Seeker>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        startPosition = rigidbody2d.position;
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        enemy = GetComponent<Enemy>();

        movementCollider = GetComponent<Collider2D>();
        
        
    }

    protected void ReduceCooldown()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown < 0)
        {
            currentCooldown = 0;
        }
    }

    protected bool IsAbilityReady()
    {
        return currentCooldown <= 0;
    }

    void ForgetTarget()
    {
        keepingTrackOfTarget = false;
        path = null;
    }

    void UpdatePath()
    {
        if (!seeker.IsDone() || state == EnemyState.Attacking) return;

        if(state == EnemyState.Walking && !returnToStart)
        {
            seeker.StartPath(rigidbody2d.position, target.position, OnPathComplete);
        }else{
            //Can't see the target, start moving back to the start position
            seeker.StartPath(rigidbody2d.position, startPosition, OnPathComplete);
            Invoke("ForgetTarget", rememberTargetTime);
        }
            
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    protected bool InRangeOfAttack()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position - rangeOffset, attackRange, targetMask);
        if (collider)
        {
            returnToStart = false;
            return true;
            
        }
        return false;
    }
    protected bool CanSeeTarget()
    {
        //Checks if we first of all are in range of the target and then checks if there is an obstacle in the way (can see the target)
        Collider2D collider = Physics2D.OverlapCircle(transform.position-rangeOffset, detectionRange, targetMask);
        if(collider)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange, obstacleMask);
            if (hit.collider)
            {
                Player player = hit.collider.gameObject.GetComponent<Player>();
                if (player)
                {
                    keepingTrackOfTarget = true;
                    lastSeenTarget = Time.time;
                    returnToStart = false;
                    return true;
                }
            }
        }
        return false;
    }
    public void SearchForPlayer()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRange*10, obstacleMask);
        if (hit.collider)
        {
            Player player = hit.collider.gameObject.GetComponent<Player>();
            if (player)
            {
                keepingTrackOfTarget = true;
                lastSeenTarget = Time.time;
                returnToStart = false;
            }
        }
    }

    protected void DecideAction()
    {
        if (state == EnemyState.Dead)
        {
            rigidbody2d.velocity = Vector2.zero;
            return;
        }
        //Cooldown for the enemy attacking


        bool canSeeTarget = CanSeeTarget();
        // Check if we are in range of attacking the player
        if (InRangeOfAttack())
        {
            state = EnemyState.Attacking;
            path = null; //Remove the path we are walking
            if (IsAbilityReady())
            {
                Attack();
            }
        }
        else if (canSeeTarget || Time.time - lastSeenTarget < rememberTargetTime)
        {
            state = EnemyState.Walking;
        }
        else
        {
            state = EnemyState.Walking;
            returnToStart = true;
            //state = EnemyState.Idle;
        }
    }

    protected virtual void Attack()
    {

    }
    // Update is called once per frame
    
    private void OnDrawGizmosSelected()
    {
        if (showGizmos == false)
            return;

        Gizmos.DrawWireSphere(transform.position - rangeOffset, detectionRange);
        Gizmos.DrawWireSphere(transform.position - rangeOffset, attackRange);

    }

    public void SetState(EnemyState state)
    {
        this.state = state;
    }

    public void LockMovement()
    {
        canMove = false;
    }
    public void UnlockMovement()
    {
        canMove = true;
    }


    protected bool TryMove(Vector2 direction)
    {
        int count = movementCollider.Cast(
            direction,
            movementFilter,
            castCollisions,
            speed*Time.fixedDeltaTime + collisionOffset
        );
        Debug.Log("count");
        if(count == 0){
            //Movement speed is determined based on the current shape of the player.
            rigidbody2d.MovePosition(rigidbody2d.position + direction*speed*Time.fixedDeltaTime);
            return true;
        }



        return true;
    }
}

public enum EnemyState
{
    Idle,
    Walking,
    Attacking,
    Dead
}
