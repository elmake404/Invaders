using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathMeter : MonoBehaviour
{
    public delegate void Empty();
    public event Empty Died;

    [SerializeField]
    private KeyTargets _tagTarget;
    [SerializeField]
    private float _amountHelths;
    private float _helths ;
    protected void ExposeHealth() => _helths = _amountHelths;
    public void Damage(float loss)
    {
        _helths -= loss;
        Debug.Log(_helths);
        Debug.Log(loss);
        if (_helths <= 0) Died?.Invoke();
    }
    public KeyTargets GetKeyTarget() => _tagTarget;
}
