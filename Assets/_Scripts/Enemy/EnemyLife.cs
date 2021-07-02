using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : HeathMeter, IEnemuPool
{
    public delegate void DataTransfer(Transform target, LevelOfComplexityOfBehavior slevelOfComplexityOfBehavior);
    public event DataTransfer ActivationEnemy;

    [SerializeField]
    private DummyDeath _dummyDeath;
    [SerializeField]
    private Transform _model;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private AnimationClip _animation;
    [SerializeField]
    private int _activeLayer, _deactiveLayer;

    [SerializeField]
    private int _enemyId;

    public bool IsRevived { get; private set; }

    private void Awake()
    {
        Died += RetornToPool;
    }
    public void Activation(Transform target, LevelOfComplexityOfBehavior slevelOfComplexityOfBehavior)
    {
        StartCoroutine(Revival(target, slevelOfComplexityOfBehavior));
    }
    private IEnumerator Revival(Transform target, LevelOfComplexityOfBehavior slevelOfComplexityOfBehavior)
    {
        float time = 0;

        if (_animation != null)
        {
            _animator.SetBool("Necromancy", true);
            yield return new WaitForSeconds(0.02f);
            _animator.SetBool("Necromancy", false);
            time = _animation.length;
        }

        yield return new WaitForSeconds(time);

        IsRevived = true;
        gameObject.layer = _activeLayer;
        ExposeHealth();
        ActivationEnemy?.Invoke(target, slevelOfComplexityOfBehavior);
    }
    public void RetornToPool()
    {
        gameObject.layer = _deactiveLayer;
        IsRevived = false;
        PoolMenedger.GetObject(_dummyDeath.name, _model.position, _model.rotation);
        PoolEnemy.Instance.ReturnToPool(this);
    }

    public int EnemyID() => _enemyId;

    public GameObject GetObject() => gameObject;
}
