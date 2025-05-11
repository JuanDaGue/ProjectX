using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string skillID;
    public string skillName;
    public float energyCost;
    public float cooldown;
    public Sprite icon;  // Changed from SpringJoint to Sprite
    public AudioClip soundEffect;
    public GameObject visualEffect;

    public abstract void Use(GameObject user);  // Added user parameter
    public abstract void Cancel();
}