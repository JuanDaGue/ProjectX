using UnityEngine;

public class Carrier : MonoBehaviour
{


    [Header("Health System")]
    [SerializeField] protected LifeSystem vidaSystem;
    
    public virtual void TakeDamage(float damage)
    {
        vidaSystem.TakeDamage(damage);
    }

    public virtual void Heal(float amount)
    {
        vidaSystem.Heal(amount);
    }

    public bool IsAlive => vidaSystem.Current > vidaSystem.Min;
}