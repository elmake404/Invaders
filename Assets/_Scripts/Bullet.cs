using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BulletCharacteristics
{
    public float FlightSpeed, TimeLife, DamagePercentage;
}

public class Bullet : MonoBehaviour 
{
    private BulletCharacteristics _characteristics;
    [SerializeField]
    private ParticleSystem _particle;
    private IEnumerator _destroy;
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _characteristics.FlightSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {

    }
    public void Initialization(BulletCharacteristics bullet)
    {
        _characteristics = bullet;
        _destroy = ReturnToPool();
        StartCoroutine(_destroy);
    }
    private IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(_characteristics.TimeLife);
        PoolBullet.Instance.ReturnToPool(this);
    }
}
