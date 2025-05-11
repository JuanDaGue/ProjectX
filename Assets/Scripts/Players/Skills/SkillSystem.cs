using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SkillSystem : MonoBehaviour
{
    [System.Serializable]
    public class SkillSlot
    {
        public string activationKey;
        public Skill skill;
        public bool isOnCooldown;
    }

    [Header("Skill Settings")]
    [SerializeField] private List<SkillSlot> skillSlots = new List<SkillSlot>();
    //[SerializeField] private float globalCooldown = 0.5f;
    public List<Skill> Skills { get; set; } = new List<Skill>();
    public List<SkillSlot> SkillSlots => skillSlots;
    private Dictionary<string, float> cooldownTimers = new Dictionary<string, float>();
    //private EnergySystem energySystem;
    
    [Header("Events")]
    public UnityEvent<string> OnSkillUsed;
    public UnityEvent<string> OnSkillReady;

    private void Awake()
    {
        InitializeCooldownDictionary();
    }

    private void InitializeCooldownDictionary()
    {
        foreach(var slot in skillSlots)
        {
            cooldownTimers[slot.skill.skillID] = 0f;
        }
    }


    public bool TryUseSkill(SkillSlot slot)
    {
        if (slot.isOnCooldown) return false;
        slot.skill.Use(gameObject);
        StartCoroutine(SkillCooldown(slot));
        OnSkillUsed?.Invoke(slot.skill.skillID);
        //GetCooldownProgress(slot.skill.skillID);
        //Debug.Log($"Skill {GetCooldownProgress(slot.skill.skillID)} used!");
        return true;
    }


    private System.Collections.IEnumerator SkillCooldown(SkillSlot slot)
    {
        slot.isOnCooldown = true;
        cooldownTimers[slot.skill.skillID] = slot.skill.cooldown;
        //Debug.Log($"Skill {slot.skill.skillID} is on cooldown for {slot.skill.cooldown} seconds.");
        while(cooldownTimers[slot.skill.skillID] > 0)
        {
            cooldownTimers[slot.skill.skillID] -= Time.deltaTime;
            //Debug.Log("CooldownTimers"+ cooldownTimers[slot.skill.skillID]);
            yield return null;
        }
        
        slot.isOnCooldown = false;
        OnSkillReady?.Invoke(slot.skill.skillID);
    }

    public float GetCooldownProgress(string skillID)
    {
        float cooldownTime=cooldownTimers.ContainsKey(skillID) ? 
            1 - (cooldownTimers[skillID] / skillSlots.Find(s => s.skill.skillID == skillID).skill.cooldown) : 
            0f;
            // Debug.Log("CoolDown Timers"+cooldownTimers.ContainsKey(skillID)+" Skill ID"+ skillID );
            // Debug.Log("Get CoolDown Progress"+cooldownTime);    
        return cooldownTime;
    }
}