using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class NoPlayable : Carrier
{
    [SerializeField] private float dps = 10f;
    [Header("No Playable Settings")]
    [SerializeField] protected float detectionRange = 150f;
    [SerializeField] protected float moveSpeed = 5f;

    [Header("UI Settings")]
    [SerializeField] private UIenemyHealth healthUI;

    protected Transform playerTarget;
    protected LifeSystem life;
    private Animator animator;
    private bool isDying = false;

    public UnityEvent OnDeath;

    protected virtual void Start()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        life = GetComponent<LifeSystem>();
        animator = GetComponent<Animator>();
        healthUI.UpdateHealth(life.Current, life.Max);
    }

    protected virtual void Update()
    {
        if (isDying) return; // No hacer nada si está muriendo

        HandleAIBehavior();
        healthUI.UpdateHealth(life.Current, life.Max);
    }

    protected virtual void HandleAIBehavior()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

        if (distanceToPlayer <= detectionRange)
        {
            animator.SetFloat("Speed", moveSpeed); // Asegúrate de que este parámetro exista en el Animator
            ChasePlayer();
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void ChasePlayer()
    {
        Vector3 direction = (playerTarget.position - transform.position).normalized;

        // Rotar hacia el jugador
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = lookRotation;
        }

        // Mover hacia el jugador
        transform.position += direction * moveSpeed * Time.deltaTime;
    }


    public override void TakeDamage(float damage)
    {
        if (isDying) return; // Evitar múltiples llamadas a muerte

        life.TakeDamage(damage);
        Debug.Log("Life is " + life.Current);

        if (life.Current <= 0)
        {
            isDying = true;
            OnDeath.Invoke();

            if (animator != null)
                animator.SetTrigger("Death"); // Trigger para animación de muerte

            StartCoroutine(DestroyAfterDeathAnimation());
        }
    }

    private IEnumerator DestroyAfterDeathAnimation()
    {
        float deathAnimLength = 1.5f; // Ajusta según la duración de la animación
        yield return new WaitForSeconds(deathAnimLength);
        Destroy(gameObject);
    }


    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit Player");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Player");
            if (collision.gameObject.TryGetComponent<LifeSystem>(out LifeSystem health))
            {
                health.TakeDamage(dps * Time.deltaTime); // Damage over time
            }
        }
    }

}
