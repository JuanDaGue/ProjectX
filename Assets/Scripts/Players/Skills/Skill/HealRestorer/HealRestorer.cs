using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/HealRestorer")]
public class HealRestorer : Skill
{
    [Header("Heal Settings")]
    [SerializeField] private float totalHeal = 50f;
    [SerializeField] private float healDuration = 3f;
    
    private LifeSystem targetLifeSystem;
    private bool isActive;
    private float healPerSecond;

    public override void Use(GameObject user)
    {
        if (!user.TryGetComponent<LifeSystem>(out targetLifeSystem))
        {
            Debug.LogError("No LifeSystem found on user!");
            return;
        }

        healPerSecond = totalHeal / healDuration;
        isActive = true;
        user.GetComponent<MonoBehaviour>().StartCoroutine(HealOverTime());
    }

    private IEnumerator HealOverTime()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < healDuration && isActive)
        {
            targetLifeSystem.Heal(healPerSecond * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        isActive = false;
    }

    public override void Cancel()
    {
        isActive = false;
    }
}