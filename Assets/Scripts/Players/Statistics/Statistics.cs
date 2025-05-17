using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class StatEvent : UnityEvent<float> { }

public  class Statistics: MonoBehaviour 
{
    [Header("Base Settings")]
    [SerializeField] protected float maxValue = 100f;
    [SerializeField] protected float minValue = 0f;
    [SerializeField] protected float currentValue = 100f;

    [Header("Events")]
    public StatEvent OnValueChanged;
    public UnityEvent OnMinReached;

    public float Max => maxValue;
    public float Min => minValue;
    public float Current => currentValue;
    public float Percentage => Mathf.Clamp01(currentValue / maxValue);

    protected virtual void Awake()
    {
        ClampCurrentValue();
    }
    //Initialize the values//
    public virtual void Initialize(float newMax, float newMin, float startValue)
    {
        maxValue = newMax;
        minValue = newMin;
        currentValue = startValue;
        ClampCurrentValue();
    }
    //Set the max value//

    public virtual void SetToMax()
    {
        currentValue = maxValue;
        NotifyValueChanged();
    }
    //Set Min Value//

    public virtual void SetToMin()
    {
        currentValue = minValue;
        NotifyValueChanged();
        OnMinReached?.Invoke();
    }
    //Add the values//

    public virtual void Add(float amount)
    {
        if (amount <= 0) return;
        currentValue += amount;
        ClampCurrentValue();
        NotifyValueChanged();
    }
    //substract values//

    public virtual void Subtract(float amount)
    {
        if (amount <= 0) return;
        // {
        //     Destroy(gameObject);
        //     return;
        // }
        currentValue -= amount;
        ClampCurrentValue();
        NotifyValueChanged();
    }
    //Encapsulare values//

    protected virtual void ClampCurrentValue()
    {
        currentValue = Mathf.Clamp(currentValue, minValue, maxValue);
        if (currentValue <= minValue) OnMinReached?.Invoke();
    }

    protected virtual void NotifyValueChanged()
    {
        OnValueChanged?.Invoke(currentValue);
    }
}
