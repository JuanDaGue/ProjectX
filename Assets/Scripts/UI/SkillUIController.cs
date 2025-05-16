using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SkillUIController : MonoBehaviour
{
    [Header("Dynamic UI Building")]
    [SerializeField] private GameObject skillUIPrefab;   // Your UI prefab with a UISkill component.
    [SerializeField] private RectTransform skillsParent; // Parent container (must be a RectTransform).
    [SerializeField] private float gapBetweenSkills = 20f; // Gap (in pixels) between each skill UI element.

    [Header("Visual Settings")]
    [SerializeField] private Color readyColor = Color.white;
    [SerializeField] private Color cooldownColor = new Color(0.9f, 0.9f, 0.9f, 0.8f);
    [SerializeField] private float colorLerpSpeed = 2f;

    private SkillSystem skillSystem;
    private Dictionary<string, UISkill> uiSkills = new Dictionary<string, UISkill>();

    private void Start()
    {
        // Find the SkillSystem in the scene.
        skillSystem = FindFirstObjectByType<SkillSystem>();
        if (skillSystem == null)
        {
            Debug.LogError("SkillSystem not found in the scene!");
            return;
        }

        // Dynamically build the UI elements.
        BuildDynamicSkillUI();
    }

    private void BuildDynamicSkillUI()
    {

        foreach (Transform child in skillsParent)
        {
            Destroy(child.gameObject);
        }

        int index = 0;
        foreach (SkillSystem.SkillSlot slot in skillSystem.SkillSlots)
        {
            // Instantiate the UI prefab under the specified parent.
            GameObject uiObj = Instantiate(skillUIPrefab, skillsParent);
            UISkill uiSkill = uiObj.GetComponent<UISkill>();
            if (uiSkill == null)
            {
                Debug.LogError("Skill UI Prefab is missing the UISkill component!");
                continue;
            }

            // Initialize the UI element with the skill data.
            uiSkill.Initialize(slot, readyColor, cooldownColor);
            uiSkills.Add(slot.skill.skillID, uiSkill);

            // Position the UI element using its RectTransform.
            RectTransform rt = uiObj.GetComponent<RectTransform>();
            if (rt != null)
            {
               
                float elementWidth = rt.sizeDelta.x;

                rt.anchoredPosition = new Vector2(index * (elementWidth + gapBetweenSkills), rt.anchoredPosition.y);
            }

  
            StartCoroutine(UpdateSkillUI(uiSkill));

            index++;
        }
    }

    /// <summary>
    /// Coroutine that continuously updates the UI element’s color and cooldown display.
    /// </summary>
    private IEnumerator UpdateSkillUI(UISkill uiSkill)
    {
        while (uiSkill != null && uiSkill.slot != null)
        {

            float cooldownProgress = skillSystem.GetCooldownProgress(uiSkill.slot.skill.skillID);
            bool isOnCooldown = uiSkill.slot.isOnCooldown;

            
            Color targetColor = isOnCooldown ?
                Color.Lerp(readyColor, cooldownColor, cooldownProgress) :
                readyColor;
            uiSkill.icon.color = Color.Lerp(uiSkill.icon.color, targetColor, Time.deltaTime * colorLerpSpeed);

            uiSkill.UpdateCooldownVisuals(cooldownProgress);
            // If the UI prefab uses a cooldown fill image, update its fill amount.
            if (uiSkill.cooldownFill != null)
            {
                uiSkill.cooldownFill.fillAmount = Mathf.Lerp(uiSkill.cooldownFill.fillAmount, cooldownProgress, Time.deltaTime * colorLerpSpeed);
            }


            yield return null;
        }
    }
}