using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Pool/PoolObj")]
public class PoolObj : MonoBehaviour {

    [SerializeField]
    private float _delayBeforeReturn;
    [SerializeField]
    private bool _selfReturn;
    
    public void ReturnToPool()
    {        
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        if (_selfReturn)
        {
            StartCoroutine(SelfReturn());
        }
    }
    private IEnumerator SelfReturn()
    {
        yield return new WaitForSeconds(_delayBeforeReturn);
        ReturnToPool();
    }
}
