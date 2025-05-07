using UnityEngine;
using System;
[Serializable]
public class PuntosPokemon
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private int _ps;
    [SerializeField]private int Attack;
    [SerializeField]private int Defense;
    [SerializeField]private int SpecialAttack;
    [SerializeField]private int Speed;
    public int PS { get => _ps; set => _ps = value; }
    public int Attack1 { get => Attack; set => Attack = value; }
    public int Defense1 { get => Defense; set => Defense = value; }
    public int SpecialAttack1 { get => SpecialAttack; set => SpecialAttack = value; }
    public int Speed1 { get => Speed; set => Speed = value; }
    public int PuntosAtaque { get => Attack; set => Attack = value; }


}
