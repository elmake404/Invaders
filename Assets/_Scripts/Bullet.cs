using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyTargets
{
    Player, Enemy, All
}
[System.Serializable]
public struct BulletCharacteristics
{
    public KeyTargets KeyTargets;
    public MeshRenderer MeshRendererModel;
    public MeshFilter MeshFilterModel;
    public ParticleSystem Particle;
    public float FlightSpeed, TimeLife, Damage;
}

public class Bullet : MonoBehaviour
{
    //[SerializeField]
    private BulletCharacteristics _characteristics;
    [SerializeField]
    private MeshRenderer _meshRendererModel;
    [SerializeField]
    private MeshFilter _meshFilterModel;
    [SerializeField]
    private List<ParticleSystem> _particlesMove;

    [SerializeField]
    private ParticleSystem _particleBlood;
    private ParticleSystem _particleActive;
    private IEnumerator _destroy;

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _characteristics.FlightSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        HeathMeter heath = other.GetComponent<HeathMeter>();
        if (heath != null)
        {
            if (_characteristics.KeyTargets == KeyTargets.All || _characteristics.KeyTargets == heath.GetKeyTarget())
            {
                if (other.gameObject.layer == 9)
                {
                    Vector3 eulerRotation = transform.eulerAngles;
                    eulerRotation.y *= (-1);
                    Quaternion rotation = Quaternion.Euler(eulerRotation);
                    PoolMenedger.GetObject(_particleBlood.name, transform.position, rotation);
                }
                heath.Damage(-_characteristics.Damage);
                ReturnToPool();
            }
        }
        if (other.tag == "ImpenetrableWall")
        {
            ReturnToPool();
        }
    }
    private void OnValidate()
    {
        if (_meshFilterModel == null || _meshRendererModel == null)
        {
            _meshFilterModel = GetComponentInChildren<MeshFilter>();
            _meshRendererModel = GetComponentInChildren<MeshRenderer>();
        }
    }
    private void ReturnToPool()
    {
        PoolBullet.Instance.ReturnToPool(this);
        //_particleActive.gameObject.SetActive(false);
        StopCoroutine(_destroy);
        _destroy = null;
    }
    public void Initialization(BulletCharacteristics bullet)
    {
        if (bullet.MeshFilterModel == null)
        {
            Debug.Log(bullet.MeshFilterModel);
        }
        _meshFilterModel.mesh = bullet.MeshFilterModel.sharedMesh;
        _meshRendererModel.materials = bullet.MeshRendererModel.sharedMaterials;

        for (int i = 0; i < _particlesMove.Count; i++)
        {
            if(_particlesMove[i]== bullet.Particle)
            Debug.Log(123);

        }
        //_particleActive = _particlesMove[_particlesMove.IndexOf(bullet.Particle)];
        //_particleActive.gameObject.SetActive(true);

        _characteristics = bullet;
        _destroy = ReturnInTimeToPool();
        StartCoroutine(_destroy);
    }
    private IEnumerator ReturnInTimeToPool()
    {
        yield return new WaitForSeconds(_characteristics.TimeLife);
        ReturnToPool();
    }

}
