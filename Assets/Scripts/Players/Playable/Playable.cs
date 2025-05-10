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
}
