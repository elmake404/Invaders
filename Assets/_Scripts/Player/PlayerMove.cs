using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static Transform Position { get; private set; }

    private Vector3 _startTouchPos, _currentPosPlayer, _targetPosPlayer;
    private Camera _cam;

    [SerializeField]
    private float _speed;
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
        PosX.x = _targetPosPlayer.x;
        transform.position = Vector3.MoveTowards(transform.position, PosX, _speed);
    }


}
