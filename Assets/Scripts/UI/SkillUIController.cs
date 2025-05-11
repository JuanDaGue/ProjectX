using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SkillUIController : MonoBehaviour
{
    [System.Serializable]
    public class UISkill
    {
        public Image icon;
        public Text keyText;
        [HideInInspector] public SkillSystem.SkillSlot skillSlot;
    }

    [Header("UI References")]
    [SerializeField] private UISkill[] skillSlotsUI;

    [Header("Visual Settings")]
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color inactiveColor = new Color(0.5f, 0.5f, 0.5f, 0.8f);
    [SerializeField] private float colorLerpSpeed = 2f;

    private SkillSystem skillSystem;
    private Dictionary<string, Image> skillIcons = new Dictionary<string, Image>();

    private void Start()
    {
        skillSystem = FindFirstObjectByType<SkillSystem>();
        InitializeUI();
    }

    private void InitializeUI()
    {
        if (skillSystem == null || skillSlotsUI.Length == 0) return;

        for (int i = 0; i < skillSlotsUI.Length; i++)
        {
            if (i >= skillSystem.SkillSlots.Count)
            {
                skillSlotsUI[i].icon.gameObject.SetActive(false);
                continue;
            }

            SkillSystem.SkillSlot slot = skillSystem.SkillSlots[i];
            skillSlotsUI[i].skillSlot = slot;
            
            // Set initial values
            skillSlotsUI[i].icon.sprite = slot.skill.icon;
            skillSlotsUI[i].keyText.text = GetKeyString(slot.activationKey);
            skillSlotsUI[i].icon.color = inactiveColor;

            // Add to dictionary for easy access
            skillIcons.Add(slot.skill.skillID, skillSlotsUI[i].icon);

            // Start cooldown tracking
            StartCoroutine(UpdateSkillState(slot));
        }
    }

    private string GetKeyString(string keyCode)
    {
        // Remove "Alpha" from number keys
        return keyCode.Replace("Alpha", "");
    }

    private IEnumerator UpdateSkillState(SkillSystem.SkillSlot slot)
    {
        Image icon = skillIcons[slot.skill.skillID];
        
        while (true)
        {
            bool isActive = slot.isOnCooldown;
            float progress = skillSystem.GetCooldownProgress(slot.skill.skillID);
            
            // Lerp color based on cooldown progress
            Color targetColor = isActive ? 
                Color.Lerp(activeColor, inactiveColor, progress) : 
                activeColor;

            icon.color = Color.Lerp(icon.color, targetColor, Time.deltaTime * colorLerpSpeed);
            
            yield return null;
        }
    }
}