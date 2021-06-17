using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private PoolEnemy _poolEnemy;
    [SerializeField]
    private LevelConditions _levelConditions;
    [SerializeField]
    private EnemyLife[] _enemyVariations;

    private void Awake()
    {
        Spawn();
    }
    private void OnValidate()
    {
        if (_enemyVariations!=null)
        {
            _levelConditions.AddEnemyVariations(_enemyVariations);
        }
    }
    private void Spawn()
    {
        int quantity = _levelConditions.GetMaxActivatedEnemy();
        foreach (var item in _enemyVariations)
        {
            _poolEnemy.CreateAPool(item,quantity);
        }
    }
}
