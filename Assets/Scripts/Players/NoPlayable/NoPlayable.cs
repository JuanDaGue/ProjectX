using UnityEngine;

public class NoPlayable : Carrier
{
    [Header("No Playable Settings")]
    [SerializeField] protected float detectionRange = 5f;
    [SerializeField] protected float moveSpeed = 3f;
    
    [Header("UI Settings")]
    [SerializeField] private UIenemyHealth healthUI;
    protected Transform playerTarget;
    protected LifeSystem life;
    protected virtual void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        life = GetComponent<LifeSystem>();
        healthUI.UpdateHealth(life.Current, life.Max);
    }

    protected virtual void Update()
    {
        HandleAIBehavior();
        healthUI.UpdateHealth(life.Current, life.Max);
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

    public override void TakeDamage(float damage)
    {
        life.TakeDamage(damage);
        
        
        if (life.Current <= 0)
        {
            Destroy(gameObject);
        }
    }
}