using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : HeathMeter, IEnemuPool
{
    public delegate void DataTransfer(Transform target, LevelOfComplexityOfBehavior slevelOfComplexityOfBehavior);
    public event DataTransfer ActivationEnemy;

    [SerializeField]
    private DummyDeath _dummyDeath;
    [SerializeField]
    private Transform _model;

    [SerializeField]
    private int _enemyId; 


    private void Awake()
    {
        Died += RetornToPool;
    }
    public void Activation(Transform target, LevelOfComplexityOfBehavior slevelOfComplexityOfBehavior)
    {
        ExposeHealth();
        ActivationEnemy?.Invoke(target, slevelOfComplexityOfBehavior);
    }
    public void RetornToPool()
    {
        PoolMenedger.GetObject(_dummyDeath.name, _model.position, _model.rotation);
        PoolEnemy.Instance.ReturnToPool(this);
    }

    public int EnemyID() => _enemyId;

    public GameObject GetObject() => gameObject;
}
