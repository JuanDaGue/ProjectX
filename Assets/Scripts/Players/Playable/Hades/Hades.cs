using UnityEngine;

public class Hades : Playable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Hades Specific")]
    [SerializeField] private float darkPower = 1.2f;
    [SerializeField] private float lifeDrainAmount = 15f;
    private LifeSystem lifeSystem;
    // private new SkillSystem skillSystem;

    private void Awake()
    {

        // lifeSystem = GetComponent<LifeSystem>();
        // skillSystem = GetComponent<SkillSystem>();
    }

    protected override void Update()
    {
        HandleSkillInput();
    }

    private void HandleSkillInput()
    {
        foreach (var slot in skillSystem.SkillSlots)
        {
            if (Input.GetKeyDown(slot.activationKey))
            {
                skillSystem.TryUseSkill(slot);
                //if (!lifeSystem.TakeDamage(slot.skill.energyCost)) return ;
                lifeSystem.TakeDamage(slot.skill.energyCost);
            }
        }
    }
}