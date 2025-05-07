using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerUIController : MonoBehaviour
{
    [Header("Health System")]
    [SerializeField] private LifeSystem healthSystem;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Image HealthCircle;

    [Header("Energy System")]
    [SerializeField] private EnergySystem energySystem;
    [SerializeField] private Image energyCircle;
    [SerializeField] private TMP_Text energyText;

    [Header("Visual Settings")]
    [SerializeField] private Color fullHealthColor = Color.green;
    [SerializeField] private Color lowHealthColor = Color.red;
    [SerializeField] private Color energyColor = Color.blue;

    private void Start()
    {
        // Initialize colors
        healthSlider.fillRect.GetComponent<Image>().color = fullHealthColor;
        energyCircle.color = energyColor;

        // Subscribe to events
        healthSystem.OnValueChanged.AddListener(UpdateHealthUI);
        energySystem.OnValueChanged.AddListener(UpdateEnergyUI);

        // Initial update
        UpdateHealthUI(healthSystem.Current);
        UpdateEnergyUI(energySystem.Current);
    }

    private void UpdateHealthUI(float currentHealth)
    {
        // Update slider
        healthSlider.value = currentHealth / healthSystem.Max;

        // Update text
        healthText.text = $"{currentHealth:F0}/{healthSystem.Max:F0}";

        // Update color
        Color healthColor = Color.Lerp(lowHealthColor, fullHealthColor, healthSlider.value);
        healthSlider.fillRect.GetComponent<Image>().color = healthColor;
    }

    private void UpdateEnergyUI(float currentEnergy)
    {
        // Update circle fill
        energyCircle.fillAmount = currentEnergy / energySystem.Max;

        // Update text
        energyText.text = $"{currentEnergy:F0}/{energySystem.Max:F0}";
    }

    public void ShowDamageEffect(float duration)
    {
        StartCoroutine(DamageEffectRoutine(duration));
    }

    private IEnumerator DamageEffectRoutine(float duration)
    {
        Image damageImage = healthSlider.GetComponent<Image>();
        Color originalColor = damageImage.color;
        damageImage.color = Color.red;

        yield return new WaitForSeconds(duration);
        damageImage.color = originalColor;
    }
}