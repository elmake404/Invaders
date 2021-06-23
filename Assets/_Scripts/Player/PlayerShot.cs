using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    public delegate void GettingAnInteger(int numberOfCartridges);
    public event GettingAnInteger Shot;

    [SerializeField]
    private Transform _shotPos;
    [SerializeField]
    private BulletCharacteristics _bulletCharacteristics;
    private BulletCharacteristics _additionalBulletCharacteristics;
    private int _numberOfCartridges;

    [SerializeField]
    private float _frequencyOfShots;

    void Awake()
    {
        GameStageEvent.StartLevel += StartShooting;
    }
    private void StartShooting()
    {
        GameStageEvent.StartLevel -= StartShooting;

        StartCoroutine(Shooting());
    }
    private IEnumerator Shooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(_frequencyOfShots);
            Bullet bullet = PoolBullet.Instance.GetBullet(_shotPos.position, _shotPos.rotation);
            BulletCharacteristics bulletCharacteristics = GetBulletCharacteristics();
            bullet.Initialization(bulletCharacteristics);
            _numberOfCartridges--;
            Shot?.Invoke(_numberOfCartridges);
        }
    }
    private BulletCharacteristics GetBulletCharacteristics()
    {
        if (_numberOfCartridges>0)
        {
            return _additionalBulletCharacteristics;
        }
        else
        {
            return _bulletCharacteristics;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        BonusWeapon bonus = other.GetComponent<BonusWeapon>();
        if (bonus != null) bonus.GetWeapon(out _additionalBulletCharacteristics,out _numberOfCartridges);
    }
}
