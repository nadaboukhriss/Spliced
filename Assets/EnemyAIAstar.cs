using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAIAstar : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private LayerMask targetMask;

    [SerializeField]
    private LayerMask obstacleMask;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float nextWaypointDistance = 1f;
    [SerializeField]
    private float detectionRange = 5f;
    [SerializeField]
    private float attackRange = 3f;
    [SerializeField]
    private Vector3 rangeOffset = Vector3.zero;
    [SerializeField]
    private float rememberTargetTime = 1f; // How many seconds to remember the target after it can no longer be seen.
    [SerializeField]
    private float attackCooldown = 1f;

    [SerializeField]
    private bool showGizmos = true;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Vector2 startPosition;
    private bool returnToStart = false;
    private bool keepingTrackOfTarget = false;

    private Seeker seeker;
    private Rigidbody2D rigidbody2d;
    private Animator animator;

    private EnemyState state;

    private float currentCooldown = 0f;

    private bool isAlive = true;
    private Enemy enemy;

    private bool canMove = true;
    private float lastSeenTarget = Mathf.NegativeInfinity;
    private Vector2 movementDirection = Vector2.zero;
    
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
        
        
    }

    void ForgetTarget()
    {
        keepingTrackOfTarget = false;
        path = null;
    }

    void UpdatePath()
    {
        if (!seeker.IsDone() || state == EnemyState.Attacking) return;

        if(state == EnemyState.Walking)
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

    private bool InRangeOfAttack()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position - rangeOffset, attackRange, targetMask);
        if (collider)
        {
            return true;
        }
        return false;
    }
    private bool CanSeeTarget()
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
                    return true;
                }
            }
        }
        return false;
    }
    public void Update()
    {
        if(state == EnemyState.Dead)
        {
            rigidbody2d.velocity = Vector2.zero;
            return;
        }
        //Cooldown for the enemy attacking
        currentCooldown -= Time.deltaTime;
        if(currentCooldown < 0)
        {
            currentCooldown = 0;
        }

        Debug.Log(animator.GetFloat("XInput").ToString() + " " + animator.GetFloat("YInput"));

        bool canSeeTarget = CanSeeTarget();
        // Check if we are in range of attacking the player
        if (InRangeOfAttack()){
            state = EnemyState.Attacking;
            path = null; //Remove the path we are walking
            if (currentCooldown <= 0)
            {
                Vector2 direction = ((Vector2)(target.position - transform.position)).normalized;

                Debug.Log("Attack direction: " + direction);
                animator.SetFloat("XInput", direction.x);
                animator.SetFloat("YInput", direction.y);
                currentCooldown = attackCooldown;
                animator.SetTrigger("Attack");
            }
        }else if(canSeeTarget || Time.time - lastSeenTarget < rememberTargetTime)
        {
            lastSeenTarget = Time.time;
            state = EnemyState.Walking;
        }
        else
        {
            state = EnemyState.Idle;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null || currentWaypoint >= path.vectorPath.Count)
        {
            //rigidbody2d.velocity = Vector2.zero;
            animator.SetBool("isWalking", false);
            state = EnemyState.Idle;
            reachedEndOfPath = true;
            return;
        }

        if(state == EnemyState.Walking && canMove)
        {

            movementDirection = ((Vector2)path.vectorPath[currentWaypoint] - rigidbody2d.position).normalized;
            rigidbody2d.AddForce(movementDirection * speed*Time.deltaTime);
            
            animator.SetBool("isWalking", true);

            animator.SetFloat("XInput", movementDirection.x);
            animator.SetFloat("YInput", movementDirection.y);

            float distance = Vector2.Distance(rigidbody2d.position, path.vectorPath[currentWaypoint]);
            if(distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }

       
    }
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

}

public enum EnemyState
{
    Idle,
    Walking,
    Attacking,
    Dead
}
