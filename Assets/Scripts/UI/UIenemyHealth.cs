using UnityEngine;
using UnityEngine.UI;

public class UIenemyHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Image healthFill;
    

    public void UpdateHealth(float current, float max)
    {
        float fillAmount = Mathf.Clamp01(current / max);
        healthFill.fillAmount = fillAmount;
    }
}
