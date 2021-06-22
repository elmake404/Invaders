using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField]
    private Transform _shotPos;
    [SerializeField]
    private BulletCharacteristics _bulletCharacteristics;

    [SerializeField]
    private float _frequencyOfShots;

    void Awake()
    {
        GameStageEvent.StartLevel += StartShoot;
    }

    private void StartShoot()
    {
        GameStageEvent.StartLevel -= StartShoot;

        StartCoroutine(Shot());
    }
    private IEnumerator Shot()
    {
        while (true)
        {
            yield return new WaitForSeconds(_frequencyOfShots);
            Bullet bullet =PoolBullet.Instance.GetBullet( _shotPos.position, _shotPos.rotation);

            bullet.Initialization(_bulletCharacteristics);
        }

    }
}
