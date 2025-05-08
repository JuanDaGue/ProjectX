using UnityEngine;

public class Warroir : Playable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Warrior Specific")]
    [SerializeField] private float attackDamage = 20f;
    [SerializeField] private float specialAttackCost = 30f;

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            PerformBasicAttack();
        }

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("Special Attack");
            TrySpecialAttack();
        }
    }

    private void PerformBasicAttack()
    {
        // Implement attack logic
        Debug.Log("Warrior basic attack!");
    }

    private void TrySpecialAttack()
    {
        if(base.TryUseSkill(0))
        {
            Debug.Log("Warrior special attack!");
            TakeDamage(10.0f);
            // Add special attack implementation
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage * 0.9f); // 10% damage reduction
    }
}