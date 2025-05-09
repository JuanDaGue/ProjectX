using UnityEngine;

/// <summary>
/// Clase controlable por el jugador que hereda de Carrier.
/// Incorpora sistemas de energía, experiencia y habilidades.
/// </summary>
public class Playable : Carrier
{
    [Header("Sistemas del jugador")]
    [SerializeField] protected EnergySystem energiaSystem;
    [SerializeField] protected XpSystem xpSystem;
    [SerializeField] protected SkillSystem skillSystem;


    protected virtual void Update()
    {
        
    }

    /// <summary>
    /// Intenta usar una habilidad según el índice.
    /// </summary>
    // public bool TryUseSkill(int skillIndex)
    // {
    //     Debug.Log("I am in Playable"+skillSystem.Skills.Count);
    //     Debug.Log("I am in Playable SkillsIndex"+skillIndex);
    //     if (skillIndex < 0 || skillIndex >= skillSystem.Skills.Count) return false;

    //     Skill skill = skillSystem.Skills[skillIndex];
    //     if (energiaSystem.TryUseEnergy(skill.energyCost))
    //     {
    //         skill.Use();
    //         return true;
    //     }
    //     return false;
    // }
}
