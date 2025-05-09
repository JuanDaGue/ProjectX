using UnityEngine;

public class EnergySystem : Statistics
{
    [Header("Energy Settings")]
    [SerializeField] private float rechargeRate = 5f;
    [SerializeField] private bool autoRegenerate = true;

    private void Update()
    {
        if (autoRegenerate && Current < Max)
        {
            Recharge(rechargeRate * Time.deltaTime);
        }
    }

    public bool TryUseEnergy(float amount)
    {
        if (Current >= amount)
        {
            Subtract(amount);
            return true;
        }
        return false;
    }

    public void Recharge(float amount)
    {
        Add(amount);
    }
//     public void AddEnergy(float amount)
// {
//     Current = Mathf.Clamp(Current + amount, Min, Max);
// }
}
