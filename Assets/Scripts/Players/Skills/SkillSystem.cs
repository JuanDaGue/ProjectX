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
    [SerializeField] private float globalCooldown = 0.5f;
    public List<Skill> Skills { get; set; } = new List<Skill>();
    
    private Dictionary<string, float> cooldownTimers = new Dictionary<string, float>();
    private EnergySystem energySystem;
    
    [Header("Events")]
    public UnityEvent<string> OnSkillUsed;
    public UnityEvent<string> OnSkillReady;

    private void Awake()
    {
        energySystem = GetComponent<EnergySystem>();
        InitializeCooldownDictionary();
    }

    private void InitializeCooldownDictionary()
    {
        foreach(var slot in skillSlots)
        {
            cooldownTimers[slot.skill.skillID] = 0f;
        }
    }

    private void Update()
    {
        HandleSkillInput();
        UpdateCooldowns();
    }

    private void HandleSkillInput()
    {
        foreach(var slot in skillSlots)
        {
            if(Input.GetKeyDown(slot.activationKey)) 
            {
                TryUseSkill(slot);
            }
        }
    }

    private void TryUseSkill(SkillSlot slot)
    {
        if(slot.isOnCooldown) return;
        if(!energySystem.TryUseEnergy(slot.skill.energyCost)) return;
        
        StartCoroutine(GlobalCooldown());
        StartCoroutine(SkillCooldown(slot));
        slot.skill.Use();
        OnSkillUsed?.Invoke(slot.skill.skillID);
    }

    private System.Collections.IEnumerator GlobalCooldown()
    {
        yield return new WaitForSeconds(globalCooldown);
    }

    private System.Collections.IEnumerator SkillCooldown(SkillSlot slot)
    {
        slot.isOnCooldown = true;
        cooldownTimers[slot.skill.skillID] = slot.skill.cooldown;
        
        while(cooldownTimers[slot.skill.skillID] > 0)
        {
            cooldownTimers[slot.skill.skillID] -= Time.deltaTime;
            yield return null;
        }
        
        slot.isOnCooldown = false;
        OnSkillReady?.Invoke(slot.skill.skillID);
    }

    private void UpdateCooldowns()
    {
        // Optional: Update UI elements here
    }

    public float GetCooldownProgress(string skillID)
    {
        return cooldownTimers.ContainsKey(skillID) ? 
            1 - (cooldownTimers[skillID] / skillSlots.Find(s => s.skill.skillID == skillID).skill.cooldown) : 
            0f;
    }
}