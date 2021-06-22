﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavEnemy : MonoBehaviour
{
    private Vector3[] _route;
    private int _namberPoints;
    private LevelOfComplexityOfBehavior _slevelOfComplexityOfBehavior;

    public void GoToTheGoal(float speed)
    {
        Vector3 NextPos = _route[_namberPoints];
        NextPos.y = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, NextPos, speed);
    }
    public bool AtTheGoal()
    {
        Vector3 NextPos = _route[_namberPoints];
        NextPos.y = transform.position.y;
        return (transform.position - NextPos).magnitude <= 0.1f;
    }
    public void NextGoal()
    {
        if (_namberPoints < _route.Length - 1)
        {
            _namberPoints++;
        }
        else
        {

           NewRoute(_slevelOfComplexityOfBehavior, true);
        }

    }
    public void AbandonmentOfThePoint(Vector3 position) => _route[_namberPoints] = position;
    public void GoTheOtherWay()
    {
        bool right = transform.position.x > _route[_namberPoints].x;
        _route[_namberPoints] = Battlefield.Instens.GetPointInTheOtherSide(_slevelOfComplexityOfBehavior, _route[_namberPoints], right);
    }
    public Vector3 GetDirection()
    {
        Vector3 NextPos = _route[_namberPoints];
        NextPos.y = transform.position.y;
        return (transform.position - NextPos).normalized;
    }
    public void NewRoute(LevelOfComplexityOfBehavior slevelOfComplexityOfBehavior, bool reCall)
    {
        _slevelOfComplexityOfBehavior = slevelOfComplexityOfBehavior;
        _namberPoints = 0;
        _route = Battlefield.Instens.GetPointsOnBattelefield(slevelOfComplexityOfBehavior, transform.position, reCall);
    }
}
