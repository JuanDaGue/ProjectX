using UnityEngine;

public abstract class Skill: ScriptableObject
{
    public string skillID;
    public string skillName;
    public float energyCost;
    public float cooldown;
    public Sprite icon;
    public AudioClip soundEffect;
    public GameObject visualEffect;

    public abstract void Use();
    public abstract void Cancel();
}