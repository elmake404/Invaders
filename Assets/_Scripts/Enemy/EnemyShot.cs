using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MaxMin
{
    public int Max, Min;
}
public class EnemyShot : MonoBehaviour
{
    [SerializeField]
    private Transform _shotPos;

    [SerializeField]
    private BulletCharacteristics _bulletCharacteristics;
    [SerializeField]
    private float _frequencyOfShots;
    [SerializeField]
    private MaxMin _numberOfShotsPerRound;

    public bool IsCanShoot { get; private set; }
    public bool IsShoot { get; private set; }

    void Start()
    {
        IsCanShoot = true;
    }

    private IEnumerator Shot()
    {
        IsShoot = true;
        int numberOfShots = Random.Range(_numberOfShotsPerRound.Min, _numberOfShotsPerRound.Max);
        while (numberOfShots > 0)
        {
            yield return new WaitForSeconds(_frequencyOfShots);
            Bullet bullet = PoolBullet.Instance.GetBullet(_shotPos.position, _shotPos.rotation);

            bullet.Initialization(_bulletCharacteristics);
            numberOfShots--;
        }
        IsCanShoot = false;

        IsShoot = false;
    }
    public void AbilityToShoot() => IsCanShoot = true;
    public void Activation()
    {
        IsCanShoot = true;
        IsShoot = false;
    }
    public void StartShot() => StartCoroutine(Shot());

}
