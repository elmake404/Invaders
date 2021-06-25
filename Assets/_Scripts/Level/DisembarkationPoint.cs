using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisembarkationPoint : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField]
    private float _radus;
    public bool CheckFree()
    {
        RaycastHit[] raycastHit = Physics.SphereCastAll(transform.position, _radus, transform.up, 0.1f, _layerMask);
        if (raycastHit.Length > 0)
        {
            return false;
        }
        return true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radus);
    }
}
