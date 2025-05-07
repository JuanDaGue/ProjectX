using UnityEngine;
using System.Collections;

public class EnergyReload : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float rechargeRate = 10f; // Energy per second
    [SerializeField] private float rechargeInterval = 0.5f; // How often to apply recharge
    [SerializeField] private EnergySystem energySystem;

    private bool isPlayerInZone = false;
    private float rechargeTimer = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
            StartCoroutine(RechargeEnergy());
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

    private IEnumerator RechargeEnergy()
    {
        while (isPlayerInZone && energySystem.Current < energySystem.Max)
        {
            energySystem.Recharge(rechargeRate * rechargeInterval);
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