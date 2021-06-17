using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEnemy : MonoBehaviour
{
   
    public static PoolEnemy Instance;

    public delegate void GetIEnemy(IEnemuPool enemu);
    public event GetIEnemy EnemyReturnToPool;

    private Dictionary<int, Queue<IEnemuPool>> _pooolEnemy = new Dictionary<int, Queue<IEnemuPool>>();
    private Dictionary<int, EnemyLife> _exampleEnemy = new Dictionary<int, EnemyLife>();

    private void Awake()
    {
        Instance = this;
    }

    //void Update()
    //{

    //}
    public void CreateAPool(EnemyLife example, int quantity)
    {
        _exampleEnemy[example.EnemyID()] = example;
        _pooolEnemy[example.EnemyID()] = new Queue<IEnemuPool>();

        for (int i = 0; i < quantity; i++)
        {
            EnemyLife enemy = Instantiate(example, transform);
            enemy.gameObject.SetActive(false);
            _pooolEnemy[example.EnemyID()].Enqueue(enemy);
        }
    }

    public void ReturnToPool(IEnemuPool enemy)
    {
        
        if (!_pooolEnemy[enemy.EnemyID()].Contains(enemy))
        {
            EnemyReturnToPool?.Invoke(enemy);

            enemy.GetObject().SetActive(false);
            _pooolEnemy[enemy.EnemyID()].Enqueue(enemy);
        }
        else
        {
            Debug.LogError("The enemy is already in the pool");
        }
    }
    public IEnemuPool GetEnemy(int EnemyID, Vector3 position, Quaternion rotation)
    {
        IEnemuPool enemy;
        if (_pooolEnemy[EnemyID].Count > 0)
        {

            enemy = _pooolEnemy[EnemyID].Dequeue();
        }
        else
        {
            enemy = Instantiate(_exampleEnemy[EnemyID], transform);
        }

        enemy.GetObject().SetActive(true);
        enemy.GetObject().transform.SetPositionAndRotation(position, rotation);

        return enemy;
    }
}
