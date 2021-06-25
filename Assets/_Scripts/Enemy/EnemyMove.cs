using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour 
{
    [SerializeField]
    private NavEnemy _navEnemy;
    [SerializeField]
    private EnemyShot _enemyShot;
    [SerializeField]
    private EnemyLife _enemyLife;
    [SerializeField]
    private Transform _objTouch;
    [SerializeField]
    private Rigidbody _rbMain;
    private Transform _positionTarget;

    [SerializeField]
    private float _speedMove, _spedRotation;
    private void Awake()
    {
        _enemyLife.ActivationEnemy += StartWar;
    }
    public bool IsMove { get; private set; }
    public int Priority { get { return _navEnemy.Priority; } }
    private void FixedUpdate()
    {
        Rotation();

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
            _navEnemy.GoToTheGoal(_speedMove);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyMove enemyFaced = other.GetComponent<EnemyMove>();
        if (enemyFaced != null && enemyFaced.Priority == Priority)
        {
            _navEnemy.IncreasePrioriti();
        }
    }
    private void Rotation()
    {
        if (_positionTarget != null)
        {
            Quaternion rotationTarget = Quaternion.LookRotation(_positionTarget.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, _spedRotation);
        }
        if (_navEnemy.GetDirection() != Vector3.zero)
            _objTouch.forward = _navEnemy.GetDirection() * (-1);
    }
    private void StartWar(Transform target, LevelOfComplexityOfBehavior slevelOfComplexityOfBehavior)
    {
        _enemyShot.Activation();
        _positionTarget = target;

        _navEnemy.NewRoute(slevelOfComplexityOfBehavior, false);
        _navEnemy.GoToTheGoal(_speedMove);
    }
}
