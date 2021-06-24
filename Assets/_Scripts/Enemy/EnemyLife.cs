using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : HeathMeter, IEnemuPool
{
    [SerializeField]
    private EnemyMove _enemyMove;
    [SerializeField]
    private int _enemyId; 


    private void Awake()
    {
        Died += RetornToPool;
    }
    public void Activation(Transform target, LevelOfComplexityOfBehavior slevelOfComplexityOfBehavior)
    {
        ExposeHealth();
        _enemyMove.StartWar(target,slevelOfComplexityOfBehavior);
    }
    public void RetornToPool()
    {        
        PoolEnemy.Instance.ReturnToPool(this);
    }

    public int EnemyID() => _enemyId;

    public GameObject GetObject() => gameObject;
}
