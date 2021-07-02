using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMove : MonoBehaviour
{
    private Vector3 _target;
    [SerializeField]
    private float _speed;
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,_target,_speed);
    }
    public void SetPositionAlongTheApplicateAxis(float z)
    {
        _target = transform.position;
        _target.z = z; 
    }
}
