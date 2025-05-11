using UnityEngine;

   // Separate class for zone behavior
    public class ZonePrefabs : MonoBehaviour
    {
        private float dps;
        private float duration;
        private float radius;
        private float currentTime;

        public void Initialize(float damagePerSecond, float duration, float radius)
        {
            this.dps = damagePerSecond;
            this.duration = duration;
            this.radius = radius;
            currentTime = 0f;
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= 0.5f) // Damage interval
            {
                ApplyDamage();
                currentTime = 0f;
            }
        }

        private void ApplyDamage()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Enemy"))
                {
                    if (col.TryGetComponent<LifeSystem>(out LifeSystem health))  // Get health component
                    if (health != null)
                    {
                        health.TakeDamage(dps * 0.5f); // Half damage per interval
                    }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
