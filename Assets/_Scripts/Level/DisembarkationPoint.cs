using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisembarkationPoint : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _enemyTake = new List<GameObject>();
    public bool IsFree { get { return _enemyTake.Count <= 0; } }
    private void Start()
    {
        PoolEnemy.Instance.EnemyReturnToPool += CheckTakeEnemy;
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyLife enemy = other.GetComponent<EnemyLife>();
        if (enemy!=null)
        {
            _enemyTake.Add(enemy.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_enemyTake.Contains(other.gameObject))
        {
            _enemyTake.Remove(gameObject);
        }
    }
    private void CheckTakeEnemy(IEnemuPool enemu) => _enemyTake.Remove(enemu.GetObject());
}
