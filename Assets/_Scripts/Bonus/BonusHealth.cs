using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHealth : MonoBehaviour
{
    [SerializeField]
    private int _addHealth;
    public float GetHelath()
    {
        Destroy(gameObject);
        return _addHealth;
    }
}
