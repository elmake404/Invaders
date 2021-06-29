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
    [SerializeField]
    private Animator _animator;
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

        if (_navEnemy.GetDistens() <= 0.07f)
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

        _animator.SetInteger("Side", GetRotation());


        _animator.SetFloat("Distense", _navEnemy.GetDistens());
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
            _objTouch.forward = _navEnemy.GetDirection();
    }
    private void StartWar(Transform target, LevelOfComplexityOfBehavior slevelOfComplexityOfBehavior)
    {
        _enemyShot.Activation();
        _positionTarget = target;

        _navEnemy.NewRoute(slevelOfComplexityOfBehavior, false);
        _navEnemy.GoToTheGoal(_speedMove);
    }
    private int GetRotation()
    {

        if (_navEnemy.GetDistens() <= 0.1f)
            return 0;

        if (_objTouch.localEulerAngles.y >= 350 && _objTouch.localEulerAngles.y <= 10)
        {
            return 8;
        }
        else if (_objTouch.localEulerAngles.y >= 10 && _objTouch.localEulerAngles.y <= 80)
        {
            return 1;
        }
        else if (_objTouch.localEulerAngles.y >= 80 && _objTouch.localEulerAngles.y <= 100)
        {
            return 2;
        }
        else if (_objTouch.localEulerAngles.y >= 100 && _objTouch.localEulerAngles.y <= 150)
        {
            return 3;
        }
        else if (_objTouch.localEulerAngles.y >= 150 && _objTouch.localEulerAngles.y <= 190)
        {
            return 4;
        }
        else if (_objTouch.localEulerAngles.y >= 190 && _objTouch.localEulerAngles.y <= 240)
        {
            return 5;
        }
        else if (_objTouch.localEulerAngles.y >= 240 && _objTouch.localEulerAngles.y <= 280)
        {
            return 6;
        }
        else
        {
            return 7;
        }
    }
}
