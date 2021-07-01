using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static Transform Position { get; private set; }

    private Vector3 _startTouchPos, _currentPosPlayer, _targetPosPlayer;
    private Camera _cam;
    private Vector3 _startPos;
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private float _speed, _Offset;
    private float _widthFarPoint
    { get { return _startPos.x + _Offset; } }
    private float _widthNearPoint
    { get { return _startPos.x - _Offset; } }

    private void Awake()
    {
        Position = transform;
    }

    void Start()
    {
        _targetPosPlayer = transform.position;
        _cam = Camera.main;
    }
    private void Update()
    {
        if (GameStage.IsGameFlowe)
        {
            if (TouchUtility.TouchCount > 0)
            {
                Touch touch = TouchUtility.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

                    _currentPosPlayer = transform.position;

                    _startTouchPos = (_cam.transform.position - ((ray.direction) *
                            ((_cam.transform.position - transform.position).z / ray.direction.z)));
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

                    if (_startTouchPos == Vector3.zero)
                    {
                        _currentPosPlayer = transform.position;

                        _startTouchPos = (_cam.transform.position - ((ray.direction) *
                                ((_cam.transform.position - transform.position).z / ray.direction.z)));
                    }

                    _targetPosPlayer = _currentPosPlayer + ((_cam.transform.position - ((ray.direction) *
                            ((_cam.transform.position - transform.position).z / ray.direction.z))) - _startTouchPos);
                }
            }
            else
            {
                _targetPosPlayer = transform.position;
            }
        }
    }
    void FixedUpdate()
    {
        Vector3 PosX = transform.position;
        PosX.x = GetPositionAbscissa(_targetPosPlayer.x);
        _animator.SetInteger("Strafe", GetDirectionMove());

        transform.position = Vector3.MoveTowards(transform.position, PosX, _speed);
    }
    private int GetDirectionMove()
    {
        if (_targetPosPlayer.x - transform.position.x == 0)
        {
            return 0;
        }
        else if (_targetPosPlayer.x > transform.position.x)
        {
            return 1;
        }
        else if (_targetPosPlayer.x < transform.position.x)
        {
            return -1;
        }
        else return 0;
    }
    private float GetPositionAbscissa(float X)
    {
        if (_widthFarPoint < X)
        {
            return _widthFarPoint;
        }
        else if (_widthNearPoint > X)
        {
            return _widthNearPoint;
        }
        else
        {
            return X;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 from = transform.position;
        Vector3 to = transform.position;
        from.x = transform.position.x - _Offset;
        to.x = transform.position.x + _Offset;
        Gizmos.DrawLine(from, to);
    }
}
