using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public Weapon(Weapon weapon)
    {
        BulletCharacteristics = weapon.BulletCharacteristics;
        NumberOfCartridges = weapon.NumberOfCartridges;
        FrequencyOfShots = weapon.FrequencyOfShots;
    }
    [SerializeField]
    public BulletCharacteristics BulletCharacteristics;
    [SerializeField]
    public int NumberOfCartridges;

    [SerializeField]
    public float FrequencyOfShots;

}
public class PlayerShot : MonoBehaviour
{
    public delegate void GettingAnInteger(int numberOfCartridges);
    public event GettingAnInteger Shot;

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Transform _shotPos;
    [SerializeField]
    private Weapon _weaponCharacteristics;
    private Weapon _additionalWeaponCharacteristics;

    void Awake()
    {
        GameStageEvent.StartLevel += StartShooting;
    }
    private void StartShooting()
    {
        GameStageEvent.StartLevel -= StartShooting;
        _animator.SetBool("StartShooting", true);
        StartCoroutine(Shooting());
    }
    private IEnumerator Shooting()
    {
        while (GameStage.IsGameFlowe)
        {
            Weapon weapon = GetBulletCharacteristics();
            yield return new WaitForSeconds(weapon.FrequencyOfShots);
            Bullet bullet = PoolBullet.Instance.GetBullet(_shotPos.position, _shotPos.rotation);
            bullet.Initialization(weapon.BulletCharacteristics);
            weapon.NumberOfCartridges--;
            Shot?.Invoke(weapon.NumberOfCartridges);
        }
    }
    private Weapon GetBulletCharacteristics()
    {
        if (_additionalWeaponCharacteristics != null && _additionalWeaponCharacteristics.NumberOfCartridges > 0)
        {
            return _additionalWeaponCharacteristics;
        }
        else
        {
            return _weaponCharacteristics;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        BonusWeapon bonus = other.GetComponent<BonusWeapon>();
        if (bonus != null) _additionalWeaponCharacteristics = bonus.GetWeapon();
    }
}
