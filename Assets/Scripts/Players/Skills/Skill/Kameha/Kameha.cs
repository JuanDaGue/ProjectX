using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/Kamehameha")]
public class Kamehameha : Skill
{
    [Header("Kamehameha Settings")]
    [SerializeField] private GameObject kamehamehaPrefab;
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private float maxChargeTime = 2f;
    [SerializeField] private float maxDamage = 100f;
    [SerializeField] private float baseDamage = 50f;

    private GameObject currentProjectile;
    private float chargeTimer;

    public override void Use(GameObject user)
    {
        if (user == null || kamehamehaPrefab == null) return;

        // Start charging
        chargeTimer = 0f;
        user.GetComponent<MonoBehaviour>().StartCoroutine(ChargingRoutine(user));
    }

    private IEnumerator ChargingRoutine(GameObject user)
    {
        // Charge phase
        while (chargeTimer < maxChargeTime && Input.GetKey(KeyCode.Mouse0))
        {
            chargeTimer += Time.deltaTime;
            // Add visual/audio feedback for charging
            yield return null;
        }

        // Release projectile
        FireProjectile(user.transform);
    }

    private void FireProjectile(Transform userTransform)
    {
        // Calculate damage based on charge time
        float damage = Mathf.Lerp(baseDamage, maxDamage, chargeTimer / maxChargeTime);
        
        Vector3 spawnPosition = userTransform.position + userTransform.forward * 2.5f + Vector3.up * 1.1f;
        currentProjectile = Instantiate(kamehamehaPrefab, spawnPosition, userTransform.rotation);
        
        KamehamehaProjectile projectile = currentProjectile.GetComponent<KamehamehaProjectile>();
        projectile.Initialize(damage, projectileSpeed, userTransform.forward);
    }

    public override void Cancel()
    {
        if (currentProjectile != null)
        {
            Destroy(currentProjectile);
        }
    }
}
