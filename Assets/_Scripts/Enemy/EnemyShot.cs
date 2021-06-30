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
    private Animator _animator;
    [SerializeField]
    private BulletCharacteristics _bulletCharacteristics;
    [SerializeField]
    private float _frequencyOfShots, _pausesBetweenShooting;
    [SerializeField]
    private MaxMin _numberOfShotsPerRound;

    [SerializeField]
    private bool _isAutomaticShooting;
    public bool IsCanShoot
    { get; private set; }
    public bool IsShoot
    { get; private set; }

    void Start()
    {
        IsCanShoot = true;
    }
    private void OnEnable()
    {
        if (_isAutomaticShooting)
        {
            StartCoroutine(AutomaticShooting());
        }
    }
    private IEnumerator Shot()
    {
        IsShoot = true;
        int numberOfShots = Random.Range(_numberOfShotsPerRound.Min, _numberOfShotsPerRound.Max);
        while (numberOfShots > 0)
        {
            yield return new WaitForSeconds(_frequencyOfShots - 0.02f);
            _animator.SetBool("Shot", true);

            CreateBullet();
            numberOfShots--;

            yield return new WaitForSeconds(0.02f);

            _animator.SetBool("Shot", false);
        }
        IsCanShoot = false;

        IsShoot = false;
    }
    private IEnumerator AutomaticShooting()
    {
        while (true)
        {
            int numberOfShots = Random.Range(_numberOfShotsPerRound.Min, _numberOfShotsPerRound.Max);

            while (numberOfShots > 0)
            {
                yield return new WaitForSeconds(_frequencyOfShots - 0.02f);
                _animator.SetBool("Shot", true);

                CreateBullet();
                numberOfShots--;

                yield return new WaitForSeconds(0.02f);

                _animator.SetBool("Shot", false);

            }

            yield return new WaitForSeconds(_pausesBetweenShooting);
        }

    }
    private void CreateBullet()
    {
        Bullet bullet = PoolBullet.Instance.GetBullet(_shotPos.position, _shotPos.rotation);

        bullet.Initialization(_bulletCharacteristics);
    }
    public void AbilityToShoot() => IsCanShoot = true;
    public void Activation()
    {
        _animator.SetBool("Shot", false);

        IsCanShoot = true;
        IsShoot = false;
    }
    public void StartShot()
    {
        if (!_isAutomaticShooting) StartCoroutine(Shot());
    }

}
