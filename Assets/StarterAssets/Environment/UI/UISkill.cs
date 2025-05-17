using UnityEngine;
using UnityEngine.UI;

public class UISkill : MonoBehaviour
{
    public Image icon;           // Main skill icon.
    public Text keyText;         // UI display for key/button prompt.
    public Image cooldownFill;   // (Optional) Radial fill indicator for cooldown.

    [HideInInspector]
    public SkillSystem.SkillSlot slot;
    [HideInInspector]
  

    // Colors for visual states.
    private Color readyColor;
    private Color cooldownColor;
    private float colorLerpSpeed;


    /// <summary>
    /// Initializes the UI element using the data from a skill slot.
    /// </summary>
    /// <param name="skillSlot">Skill slot data from SkillSystem</param>
    /// <param name="readyColor">Color when the skill is ready</param>
    /// <param name="cooldownColor">Color when the skill is cooling down</param>
   public void Initialize(SkillSystem.SkillSlot slot, Color readyColor, Color cooldownColor, float colorLerpSpeed = 2f)
    {

                this.slot = slot;
        this.readyColor = readyColor;
        this.cooldownColor = cooldownColor;
        this.colorLerpSpeed = colorLerpSpeed;
        if (icon != null)
        {
            icon.sprite = slot.skill.icon;
            // Start with the cooldown color (or adjust as needed)
            icon.color = cooldownColor;
        }
        if (keyText != null)
        {
            // Remove "Alpha" from the key string
            keyText.text = slot.activationKey.Replace("Alpha", "");
        }
        if (cooldownFill != null)
        {
            cooldownFill.fillAmount = 0f;
        }
    }

    public void UpdateCooldownVisuals(float cooldownProgress)
    {
        bool isOnCooldown = slot.isOnCooldown;
        Color targetColor = isOnCooldown ?
            Color.Lerp(readyColor, cooldownColor, cooldownProgress) : readyColor;

        if (icon != null)
        {
            icon.color = Color.Lerp(icon.color, targetColor, Time.deltaTime * colorLerpSpeed);
        }

        if (cooldownFill != null)
        {
            cooldownFill.fillAmount = Mathf.Lerp(cooldownFill.fillAmount, cooldownProgress, Time.deltaTime * colorLerpSpeed);
        }
    }

}