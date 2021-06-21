using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private LevelOfComplexityOfBehavior _slevelOfComplexityOfBehavior;
    [SerializeField]
    private Rigidbody _rbMain;
    private Transform _positionTarget;
    [SerializeField]
    private EnemyShot _enemyShot;
    [SerializeField]
    private EnemyMove _enemyFaced;
    [SerializeField]
    private Vector3[] _route;

    [SerializeField]
    private float _speedMove, _spedRotation;
    private float _movingMass = 50, _stopingMass = 5;
    [SerializeField]
    private int _namberPoints;

    public bool IsMove; /*{ get; private set; }*/
    private void FixedUpdate()
    {
        Rotation();
        if (IsMove)
        {
            Vector3 NextPos = _route[_namberPoints];
            NextPos.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, NextPos, _speedMove);

            if ((transform.position - NextPos).magnitude <= 0.1f)
            {
                if (_enemyShot.IsCanShoot)
                {
                    if (!_enemyShot.IsShoot)
                        _enemyShot.StartShot();
                }
                else
                {
                    if (_namberPoints < _route.Length - 1)
                    {
                        _enemyShot.AbilityToShoot();
                        _namberPoints++;
                    }
                    else
                    {
                        _enemyShot.AbilityToShoot();

                        _namberPoints = 0;
                        _route = Battlefield.Instens.GetPointsOnBattelefield(_slevelOfComplexityOfBehavior, transform.position, true);
                    }
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
        _slevelOfComplexityOfBehavior = slevelOfComplexityOfBehavior;

        _route = Battlefield.Instens.GetPointsOnBattelefield(_slevelOfComplexityOfBehavior, transform.position, false);
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
