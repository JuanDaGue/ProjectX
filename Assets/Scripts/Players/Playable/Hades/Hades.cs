using UnityEngine;

public class Hades : Playable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Hades Specific")]
    [SerializeField] private float darkPower = 1.2f;
    [SerializeField] private float lifeDrainAmount = 15f;

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            TryLifeDrain();
        }
    }

    private void TryLifeDrain()
    {
        if(TryUseSkill(1))
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 5f);
            foreach(var enemy in enemies)
            {
                if(enemy.TryGetComponent<Carrier>(out var enemyHealth))
                {
                    enemyHealth.TakeDamage(lifeDrainAmount);
                    Heal(lifeDrainAmount * 0.5f); // Heal 50% of damage dealt
                }
            }
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage * darkPower); // Takes extra damage
    }
}