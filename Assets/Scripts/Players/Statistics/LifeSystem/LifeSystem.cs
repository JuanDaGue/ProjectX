using UnityEngine;
using UnityEngine.Events;

public class LifeSystem : Statistics
{
    [Header("Health Events")]
    public UnityEvent OnDeath;

    public virtual void TakeDamage(float damage)
    {
        if (damage < 0)
        {
            //OnDeath.Invoke();
            Destroy(gameObject);
            return;
        }
        Subtract(damage);
        if (Current <= Min) OnDeath?.Invoke();
    }

    public void Heal(float amount)
    {
        if (amount < 0) return;
        Add(amount);
    }
        public bool CanTakeDamage(float amount)
    {
        if (Current >= amount)
            {
                Subtract(amount);
                return true;
            }
        return false;
    }

}