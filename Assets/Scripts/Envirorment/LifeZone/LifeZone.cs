using UnityEngine;
using System.Collections;

public class LifeZone : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float rechargeRate = 10f; // Energy per second
    [SerializeField] private float rechargeInterval = 0.5f; // How often to apply recharge
    [SerializeField] private LifeSystem lifeSystem; // Reference to the LifeSystem component
    
    private bool isPlayerInZone = false;
    //private float rechargeTimer = 0f;

    private void Awake()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lifeSystem= other.GetComponent<LifeSystem>();
            isPlayerInZone = true;
            StartCoroutine(RechargeLife());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            isPlayerInZone = false;
            StopAllCoroutines();
        }
    }
    private IEnumerator RechargeLife()
    {
        while (isPlayerInZone && lifeSystem.Current < lifeSystem.Max)
        {
            lifeSystem.Heal(rechargeRate * rechargeInterval);
            yield return new WaitForSeconds(rechargeInterval);
        }
    }
    // Visual feedback (optional)
    private void Update()
    {
        if (isPlayerInZone)
        {
            // Add visual effects here
        }
    }
}
