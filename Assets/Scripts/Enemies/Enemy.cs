using UnityEngine;

/// <summary>
/// Base class for enemies
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected float moveSpeed = 2.0f;
    [SerializeField] protected bool canMove = true;

    [Header("Behaviour")]
    [SerializeField] protected Transform target;
    [SerializeField] float maximumAttackRange = 5.0f;
    [SerializeField] bool doesAttack = false;
    [SerializeField] protected bool moveWhileAttacking = false;


    [Header("Effects")]
    [SerializeField] GameObject attackEffect;

    [Header("Line of Sight Settings")]
    [SerializeField] bool lineOfSightToAttack = true;
    [SerializeField] protected bool needsLineOfSightToMove = true;
    [SerializeField] LayerMask hitWithLineOfSight;
    
    protected bool isIdle = false;
    protected bool isAttacking = false;
    protected bool isMoving = false;
    protected Rigidbody enemyRigidbody = null;
    protected EnemyAttacker attacker;

    void Awake()
    {
        SetUpComponents();
    }

    void LateUpdate()
    {
        HandleMovement();
        HandleActions();
    }

    protected virtual void SetUpComponents()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        attacker = GetComponent<EnemyAttacker>();
        if (target == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    protected virtual void HandleMovement()
    {
        bool attackMove = moveWhileAttacking == true || isAttacking == false;

        bool hasLineOfSight = true;

        if (needsLineOfSightToMove)
        {
            hasLineOfSight = HasLineOfSight();
        }

        if (canMove && enemyRigidbody != null && attackMove && hasLineOfSight)
        {
            Vector3 desiredMovement = CalculateDesiredMovement();
            Quaternion desiredRotation = CalculateDesiredRotation();

            enemyRigidbody.velocity = Vector3.zero;
            enemyRigidbody.angularVelocity = Vector3.zero;

            enemyRigidbody.MovePosition(desiredMovement);
            enemyRigidbody.MoveRotation(desiredRotation);

            isMoving = true;
        }
        else if (isAttacking)
        {
            isMoving = false;
            isIdle = false;
        }
        else
        {
            if (enemyRigidbody != null)
            {
                enemyRigidbody.velocity = Vector3.zero;
                enemyRigidbody.angularVelocity = Vector3.zero;
            }

            isMoving = false;
            isIdle = true;
        }
    }

    protected virtual void HandleActions()
    {
        TryToAttack();
    }

    protected virtual void TryToAttack()
    {
        if (doesAttack && attacker != null && target != null && (target.position - transform.position).magnitude < maximumAttackRange)
        {
            if (!lineOfSightToAttack || (lineOfSightToAttack && HasLineOfSight()))
            {
                isAttacking = attacker.Attack();
                if (isAttacking && attackEffect != null)
                {
                    Instantiate(attackEffect, transform.position, Quaternion.identity, null);
                }
            }
            else
            {
                isAttacking = false;
            }
        }
        else
        {
            isAttacking = false;
        }
    }

    protected virtual Vector3 CalculateDesiredMovement()
    {
        return transform.position;
    }


    protected virtual Quaternion CalculateDesiredRotation()
    {
        return transform.rotation;
    }

    protected virtual bool HasLineOfSight()
    {
        if (target != null)
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = new Ray(transform.position, target.position - transform.position);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitWithLineOfSight))
            {
                if (hit.transform == target || target.IsChildOf(hit.transform))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
