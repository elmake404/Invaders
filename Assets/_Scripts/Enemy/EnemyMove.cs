using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rbMain;
    private PlayerLife _playerLife;
    private Transform _positionPlayer 
    { get { return _playerLife.transform; } }
    [SerializeField]
    private EnemyShot _enemyShot;
    private EnemyMove _enemyFaced;
    private Vector3[] _route;

    [SerializeField]
    private float _speedMove,_spedRotation;
    private float _movingMass = 50, _stopingMass = 5;
    private int _namberPoints;

    public bool IsMove { get; private set; }
    private void Start()
    {
        _playerLife = FindObjectOfType<PlayerLife>();
        _route = Battlefield.Instens.GetPointsOnBattelefield(5, transform.position);
        StartMoving();
    }
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
                    if(!_enemyShot.IsShoot)
                    _enemyShot.StartShot();
                }
                else
                {
                    if (_namberPoints < _route.Length - 1)
                    {
                        _enemyShot.AbilityToShoot();
                        _namberPoints++;
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
        Quaternion rotationTarget = Quaternion.LookRotation(_positionPlayer.position-transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation,rotationTarget,_spedRotation);
    }
}
