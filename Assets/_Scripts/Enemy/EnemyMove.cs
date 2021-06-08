using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rbMain;
    [SerializeField]
    private PlayerLife _playerLife;

    [SerializeField]
    private Vector3[] _route;

    [SerializeField]
    private float _speedMove;
    private int _namberPoints;
    private void Start()
    {
        _route = Battlefield.Instens.GetPointsOnBattelefield(5, transform.position);
    }
    private void FixedUpdate()
    {
        Vector3 NextPos = _route[_namberPoints];
        NextPos.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, NextPos, _speedMove);
        if (_namberPoints < _route.Length - 1 && (transform.position - NextPos).magnitude <= 0.1f) _namberPoints++;
    }
}
