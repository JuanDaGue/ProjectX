using UnityEngine;
using UnityEngine.Events;

public class LifeSystem : Statistics
{
    [Header("Health Events")]
    public UnityEvent OnDeath;

    public void TakeDamage(float damage)
    {
        if (damage < 0) return;
        Subtract(damage);
        if (Current <= Min) OnDeath?.Invoke();
    }

    public void Heal(float amount)
    {
        if (amount < 0) return;
        Add(amount);
    }
}