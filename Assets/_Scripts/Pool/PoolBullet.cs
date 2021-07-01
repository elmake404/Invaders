using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBullet : MonoBehaviour
{
    public static PoolBullet Instance;

    [SerializeField]
    private Bullet _prefabBullet;
    private Queue<Bullet> _bulletsPool = new Queue<Bullet>();

    [SerializeField]
    private int _count;
    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < _count; i++)
        {
            Bullet bullet = Instantiate(_prefabBullet, transform);
            bullet.gameObject.SetActive(false);
            _bulletsPool.Enqueue(bullet);
        }
    }
    public Bullet GetBullet(Vector3 position, Quaternion rotation)
    {
        Bullet bullet;
        
        if (_bulletsPool.Count > 0)
        {

            bullet = _bulletsPool.Dequeue();
        }
        else
        {
            bullet = Instantiate(_prefabBullet, transform);
        }

        bullet.gameObject.SetActive(true);
        bullet.transform.SetPositionAndRotation(position, rotation);

        return bullet;
    }
    public void ReturnToPool(Bullet bullet)
    {
        if (!_bulletsPool.Contains(bullet))
        {
            _bulletsPool.Enqueue(bullet);
            bullet.gameObject.SetActive(false);
        }
    }
}
