using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusWeapon : MonoBehaviour
{
    [SerializeField]
    private Weapon _additionalWeaponCharacteristics;
    public Weapon GetWeapon()
    {
        gameObject.SetActive(false);

        return new Weapon( _additionalWeaponCharacteristics);
    }
}
