using UnityEngine;

public class KamehamehaProjectile : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 0.2f;
    [SerializeField] private GameObject impactEffect;

    private float damage;
    private float speed;
    private Vector3 direction;

    public void Initialize(float damage, float speed, Vector3 direction)
    {
        this.damage = damage;
        this.speed = speed;
        this.direction = direction;
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Kameha: " + other.name + "   The tag is " + other.tag);
        
        // Check if the collider is an impact target.
        if (other.CompareTag("Terrain") || other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            Debug.Log("Kameha: Collision with " + other.name);

            // If the target has a LifeSystem component, apply damage.
            if (other.TryGetComponent<LifeSystem>(out var lifeSystem))
            {
                lifeSystem.TakeDamage(damage);
            }
            

            Destroy(gameObject, destroyDelay);
        }
    }

        private void OnDestroy()
    {
        if (impactEffect != null)
        {
            GameObject effectObject = Instantiate(impactEffect, transform.position, Quaternion.identity);
            ParticleSystem effect = effectObject.GetComponent<ParticleSystem>();
            if (effect != null)
            {
                effect.Play();
                Destroy(effectObject, effect.main.duration);
            }
        }
    }
}