using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusWeapon : MonoBehaviour
{
    [SerializeField]
    private BulletCharacteristics _additionalBulletCharacteristics;
    [SerializeField]
    private int _numberOfCartridges;
    public void GetWeapon(out BulletCharacteristics bulletCharacteristics,out int numberOfCaridges)
    {
        bulletCharacteristics = _additionalBulletCharacteristics;
        numberOfCaridges = _numberOfCartridges;
        gameObject.SetActive(false);
    }
}
