using UnityEngine;

public class KamehamehaProjectile : MonoBehaviour
{

    [SerializeField] private float destroyDelay = 0.2f;
    [SerializeField] private ParticleSystem impactEffect;
    
    private float damage;
    private float speed;
    private Vector3 direction;

    public void Initialize(float damage, float speed, Vector3 direction)
    {
        this.damage = damage;
        this.speed = speed;
        this.direction = direction;
        Destroy(gameObject, 5f); // Auto-destroy after 5 seconds
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain") || other.CompareTag("Enemy"))
        {
            // Apply damage to enemies
            if (other.TryGetComponent<LifeSystem>(out var enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
            }

            // Play impact effect
            if (impactEffect != null)
            {
                ParticleSystem effect = Instantiate(impactEffect, transform.position, Quaternion.identity);
                effect.Play();
                Destroy(effect.gameObject, effect.main.duration);
            }

            Destroy(gameObject, destroyDelay);
        }
    }
}