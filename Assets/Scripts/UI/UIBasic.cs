using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBasic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Health System")]
    [SerializeField]private TextMeshProUGUI textMeshProUGUI;
    [SerializeField]private LifeSystem lifeSystem;
    [SerializeField]private Image lifeBar;
    [SerializeField] private Image HealthCircle;
    [Header("Energy System")]
    [SerializeField]private Image EnergyBar;
    [SerializeField]private Image EnergyArea;
    [SerializeField]private EnergySystem energySystem;
    
    [Header("Visual Energy Settings")]
    [SerializeField] private Slider EnergySlider;
    [SerializeField] private Color fullEnergyr = Color.green;
    [SerializeField] private Color lowEnergyr = Color.red;
    [SerializeField] private Color HealthColor = Color.blue;
    [SerializeField] private SkillSystem skillSystem;
    [SerializeField] private Image EnergyCircleCooldown;
    
    void Start()
    {
        EnergySlider.fillRect.GetComponent<Image>().color = fullEnergyr;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update Lifesystem and EnergySystem"+ lifeSystem.Max + " " + lifeSystem.Current + " " + energySystem.Max + " " + energySystem.Current);
        if (lifeSystem != null )
        {
            // textMeshProUGUI.text = lifeSystem.Current.ToString("F0") + "/" + lifeSystem.Max.ToString("F0");
            lifeBar.fillAmount = lifeSystem.Current / lifeSystem.Max;
            HealthCircle.fillAmount = lifeSystem.Current / lifeSystem.Max;
        }
        if (energySystem != null && EnergyBar != null)
        {
            //EnergyBar.fillAmount = energySystem.Current / energySystem.Max;
            EnergySlider.value = energySystem.Current / energySystem.Max;

            Color energyColor = Color.Lerp(lowEnergyr, fullEnergyr, EnergySlider.value);
            EnergyBar.fillAmount = energySystem.Current / energySystem.Max;
            EnergyBar.color = energyColor;
            EnergyArea.color=energyColor;
            EnergySlider.fillRect.GetComponent<Image>().color = energyColor;
        }
        if (skillSystem != null && EnergyCircleCooldown != null)
        {
            foreach (var slot in skillSystem.SkillSlots)
            {
                if (slot.isOnCooldown)
                {
                    EnergyCircleCooldown.fillAmount = skillSystem.GetCooldownProgress(slot.skill.skillID);
                }
                else
                {
                    EnergyCircleCooldown.fillAmount = 1f;
                }
            }
        }

    }
}
