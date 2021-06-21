﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private NavEnemy _navEnemy;
    [SerializeField]
    private Rigidbody _rbMain;
    private Transform _positionTarget;
    [SerializeField]
    private EnemyShot _enemyShot;
    private EnemyMove _enemyFaced;

    [SerializeField]
    private float _speedMove, _spedRotation;
    private float _movingMass = 50, _stopingMass = 5;

    public bool IsMove { get; private set; }
    private void FixedUpdate()
    {
        Rotation();
        _rbMain.velocity = Vector3.zero;

        if (IsMove)
        {
            _navEnemy.GoToTheGoal(_speedMove);

            if (_navEnemy.AtTheGoal())
            {
                if (_enemyShot.IsCanShoot)
                {
                    if (!_enemyShot.IsShoot)
                        _enemyShot.StartShot();
                }
                else
                {
                    _enemyShot.AbilityToShoot();
                    _navEnemy.NextGoal();
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (_enemyFaced == null)
        {
            _enemyFaced = other.GetComponent<EnemyMove>();

        }
        else
        {
            if (_enemyFaced.IsMove)
            {
                StopMovig();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_enemyFaced != null && _enemyFaced.gameObject == other.gameObject)
        {
            _enemyFaced = null;
            StartMoving();
        }
    }
    private void StopMovig()
    {
        IsMove = false;
        _rbMain.mass = _stopingMass;
    }
    private void StartMoving()
    {
        IsMove = true;
        _rbMain.velocity = Vector3.zero;
        _rbMain.mass = _movingMass;
    }
    private void Rotation()
    {
        if (_positionTarget != null)
        {
            Quaternion rotationTarget = Quaternion.LookRotation(_positionTarget.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, _spedRotation);
        }
    }
    public void StartWar(Transform target, LevelOfComplexityOfBehavior slevelOfComplexityOfBehavior)
    {
        PoolEnemy.Instance.EnemyReturnToPool -= CheckTakeEnemy;

        _enemyShot.Activation();
        _positionTarget = target;

        _navEnemy.NewRoute(slevelOfComplexityOfBehavior,false);
        StartMoving();
        PoolEnemy.Instance.EnemyReturnToPool += CheckTakeEnemy;

    }
    private void CheckTakeEnemy(IEnemuPool enemu)
    {
        if (_enemyFaced != null && _enemyFaced.gameObject == enemu.GetObject())
        {
            _enemyFaced = null;
            StartMoving();
        }
    }


}
