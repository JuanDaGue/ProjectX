using UnityEngine;

[CreateAssetMenu(menuName = "Skills/DamageZoneSkill")]
public class DamageZoneSkill : Skill
{
    [Header("Damage Zone Settings")]
    [SerializeField] private GameObject zonePrefab;
    [SerializeField] private float zoneDuration = 5f;
    [SerializeField] private float damagePerSecond = 10f;
    [SerializeField] private float zoneRadius = 3f;

    public override void Use(GameObject user)
    {
        if (zonePrefab == null || user == null)
        {
            Debug.LogWarning("DamageZoneSkill: Missing required components!");
            return;
        }

        Vector3 spawnPosition = user.transform.position + user.transform.forward * 2f;
        spawnPosition.y = 0.5f; 
        GameObject zoneInstance = Instantiate(zonePrefab, spawnPosition, Quaternion.identity);
        zoneInstance.transform.localScale = Vector3.one * zoneRadius * 2;
    
        ZonePrefabs damageZone = zoneInstance.AddComponent<ZonePrefabs>();
        damageZone.Initialize(damagePerSecond, zoneDuration, zoneRadius);

        Destroy(zoneInstance, zoneDuration);
    }

    public override void Cancel()
    {
       
    }

}