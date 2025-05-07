using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/Teleport")]
public class Teleport : Skill
{

    [Header("Teleport Settings")]
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private float castTime = 0.5f;
    [SerializeField] private LayerMask validLayers;

    private Transform playerTransform;
    private LifeSystem healthSystem;

    public override void Use()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        healthSystem = playerTransform.GetComponent<LifeSystem>();
        // playerTransform.StartCoroutine(TeleportRoutine());
    }

    private IEnumerator TeleportRoutine()
    {
        Vector3 startPosition = playerTransform.position;
        Vector3 targetPosition = GetTargetPosition();
        
        float elapsedTime = 0f;
        while(elapsedTime < castTime)
        {
            if(Input.GetMouseButton(1)) // Right click to cancel
            {
                Cancel();
                yield break;
            }
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        ExecuteTeleport(targetPosition);
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        
        Vector3 direction = (mousePosition - playerTransform.position).normalized;
        float distance = Mathf.Min(Vector3.Distance(playerTransform.position, mousePosition), maxDistance);
        
        return playerTransform.position + direction * distance;
    }

    private void ExecuteTeleport(Vector3 targetPosition)
    {
        playerTransform.position = targetPosition;
        healthSystem.TakeDamage(energyCost * 0.2f); // 20% energy cost as health cost
    }

    public override void Cancel()
    {
        // Implement any cancellation effects
    }
}
