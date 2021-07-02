using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyTargets
{
    Player,Enemy,All
}
[System.Serializable]
public struct BulletCharacteristics
{
    public KeyTargets KeyTargets;
    public float FlightSpeed, TimeLife, Damage;
}

public class Bullet : MonoBehaviour 
{
    [SerializeField]
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
        HeathMeter heath = other.GetComponent<HeathMeter>();
        if (heath!=null)
        {
            if (_characteristics.KeyTargets == KeyTargets.All || _characteristics.KeyTargets == heath.GetKeyTarget())
            {
                if(other.gameObject.layer==9)
                {
                    Vector3 eulerRotation = transform.eulerAngles;
                    eulerRotation.y *= (-1);
                    Quaternion rotation = Quaternion.Euler(eulerRotation);
                    PoolMenedger.GetObject(_particle.name,transform.position,rotation);
                }
                heath.Damage(-_characteristics.Damage);
                ReturnToPool();
            }
        }
        if (other.tag== "ImpenetrableWall")
        {
            ReturnToPool();
        }
    }
    public void Initialization(BulletCharacteristics bullet)
    {
        _characteristics = bullet;
        _destroy = ReturnInTimeToPool();
        StartCoroutine(_destroy);
    }
    private IEnumerator ReturnInTimeToPool()
    {
        yield return new WaitForSeconds(_characteristics.TimeLife);
        ReturnToPool();
    }
    private  void ReturnToPool()
    {
        PoolBullet.Instance.ReturnToPool(this);
        StopCoroutine(_destroy);
        _destroy = null;
    }
}
