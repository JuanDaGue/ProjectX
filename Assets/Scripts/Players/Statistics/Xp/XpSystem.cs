using UnityEngine;
using UnityEngine.Events;

public class XpSystem : Statistics
{
    [System.Serializable]
    public class LevelUpEvent : UnityEvent<int> { }

    [Header("XP Settings")]
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int xpToNextLevel = 100;

    [Header("XP Events")]
    public LevelUpEvent OnLevelUp;

    protected override void Awake()
    {
        base.Awake();
        maxValue = xpToNextLevel;
        minValue = 0;
    }

    public override void Add(float amount)
    {
        base.Add(amount);
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        while (currentValue >= xpToNextLevel)
        {
            currentValue -= xpToNextLevel;
            currentLevel++;
            xpToNextLevel = CalculateRequiredXP();
            maxValue = xpToNextLevel;
            OnLevelUp?.Invoke(currentLevel);
        }
    }

    private int CalculateRequiredXP()
    {
        return Mathf.FloorToInt(100 * Mathf.Pow(1.5f, currentLevel));
    }

    public int CurrentLevel => currentLevel;
    public int XPToNextLevel => xpToNextLevel;
}