using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHealth : MonoBehaviour
{
    [SerializeField]
    private int _addHealth;
    public float GetHelath()
    {
       gameObject.SetActive(false);
        return _addHealth;
    }
}
