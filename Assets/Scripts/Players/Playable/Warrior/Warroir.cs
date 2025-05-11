using UnityEngine;

public class Warroir : Playable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Warrior Specific")]
    //private new SkillSystem skillSystem;
    private EnergySystem energySystem;
    private LifeSystem lifeSystem;
    private void Awake()
    {
        energySystem = GetComponent<EnergySystem>();
        lifeSystem = GetComponent<LifeSystem>();
        //skillSystem = GetComponent<SkillSystem>();
    }

    protected override void Update()
    {

        HandleSkillInput();
    }


    private void HandleSkillInput()
    {
        foreach (var slot in skillSystem.SkillSlots)
        {
            if (Input.GetKeyDown(slot.activationKey) && !slot.isOnCooldown )
            {
                skillSystem.TryUseSkill(slot);
                if (!energySystem.TryUseEnergy(slot.skill.energyCost)) return ;
                skillSystem.GetCooldownProgress(slot.skill.skillID);
            }
        }
    }
}