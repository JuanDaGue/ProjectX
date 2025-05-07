using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class DamageZone : MonoBehaviour
{
    [Header("Zone Settings")]
    public float startRadius = 15f;
    public float endRadius = 5f;
    public float shrinkDuration = 600f; // Total time to shrink zone
    public float damagePerSecond = 2f;

    [Header("References")]
    public Transform player;
    public PostProcessVolume stormVolume;
    public SphereCollider zoneCollider;
    public Transform zoneVisual;
    private float currentRadius;
    private float timer;

    void Start()
    {
        currentRadius = startRadius;
        transform.localScale = Vector3.one * startRadius * 2f;
        if (zoneCollider == null)
        {
            zoneCollider = gameObject.AddComponent<SphereCollider>();
            zoneCollider.isTrigger = true;
        }
    }

void Update()
{
    timer += Time.deltaTime;
    float t = Mathf.Clamp01(timer / shrinkDuration);
    currentRadius = Mathf.Lerp(startRadius, endRadius, t);

    float visualScale = currentRadius * 2f;
    transform.localScale = Vector3.one * visualScale;

    if (zoneVisual != null)
    {
        zoneVisual.localScale = Vector3.one * visualScale;
    }
    if (player != null && stormVolume != null)
        {
            float dist = Vector3.Distance(player.position, transform.position);
            stormVolume.enabled = dist > currentRadius;
        }    
}

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float distance = Vector3.Distance(other.transform.position, transform.position);
            if (distance > currentRadius)
            {
                // Deal damage over time
                other.GetComponent<LifeSystem>()?.TakeDamage(damagePerSecond * Time.deltaTime);
                
                Debug.Log("Damage dealt to player: " + other.name + " | Damage: " + damagePerSecond * Time.deltaTime);

                
            }
        }
    }
}
