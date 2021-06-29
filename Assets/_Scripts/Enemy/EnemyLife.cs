﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : HeathMeter, IEnemuPool
{
    public delegate void DataTransfer(Transform target, LevelOfComplexityOfBehavior slevelOfComplexityOfBehavior);
    public event DataTransfer ActivationEnemy;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private int _enemyId; 


    private void Awake()
    {
        Died += RetornToPool;
    }
    public void Activation(Transform target, LevelOfComplexityOfBehavior slevelOfComplexityOfBehavior)
    {
        _animator.SetBool("Shot", false);
        ExposeHealth();
        ActivationEnemy?.Invoke(target, slevelOfComplexityOfBehavior);
    }
    public void RetornToPool()
    {
        PoolEnemy.Instance.ReturnToPool(this);
    }

    public int EnemyID() => _enemyId;

    public GameObject GetObject() => gameObject;
}
