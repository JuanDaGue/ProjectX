using UnityEngine;

public class NoPlayable : Carrier
{
    [Header("AI Settings")]
    [SerializeField] protected float detectionRange = 5f;
    [SerializeField] protected float moveSpeed = 3f;
    
    protected Transform playerTarget;

    protected virtual void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        HandleAIBehavior();
    }

    protected virtual void HandleAIBehavior()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTarget.position);
        
        if(distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction = (playerTarget.position - transform.position).normalized;
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }
}