using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathMeter : MonoBehaviour
{
    public delegate void Empty();
    public event Empty Died;

    public delegate void PassTheNumber(float number);
    public event PassTheNumber HealthChange;

    [SerializeField]
    private KeyTargets _tagTarget;
    [SerializeField]
    private float _amountHelths;
    private float _helth;
    protected void ExposeHealth() => _helth = _amountHelths;
    public void Damage(float loss)
    {
        _helth += loss;
        if (_helth <= 0) Died?.Invoke();
        if (_helth > _amountHelths) ExposeHealth();
        HealthChange?.Invoke(_helth/_amountHelths);
    }
    public KeyTargets GetKeyTarget() => _tagTarget;
    protected void AddHealth(float health)
    {
        _helth += health;
    }

}
