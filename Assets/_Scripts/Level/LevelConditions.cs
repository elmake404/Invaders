using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct SpawnCharatristic
{
    [HideInInspector]
    public string Name;
    [HideInInspector]
    public EnemyLife Enemy;
    public int Easy, Normal, Hard, Random;
    public int CountEnemy
    {
        get
        {
            return (Easy + Normal + Hard + Random);
        }
    }
}
[System.Serializable]
public class AttackWaves
{
    [HideInInspector]
    public string Name;
    [SerializeField]
    private int _maximNumumberOfLivingEnemy; public int MaximNumumberOfLivingEnemy
    { get { return _maximNumumberOfLivingEnemy; } }

    [SerializeField]
    public List<SpawnCharatristic> Charatristic;


    public void AddSpawn(EnemyLife enemy)
    {
        SpawnCharatristic charatristic = new SpawnCharatristic();
        charatristic.Enemy = enemy;
        charatristic.Name = enemy.name;
        Charatristic.Add(charatristic);
    }
}

public class LevelConditions : MonoBehaviour
{
    [SerializeField]
    private AttackWaves[] _attackWaves;
    private AttackWaves _attackWave
    { get { return _attackWaves[_numberWave]; } }
    private Transform _targetEnemy;
    [SerializeField]
    private DisembarkationPoint[] _disembarkationPoints;
    private List<IEnemuPool> _enemiesInGame = new List<IEnemuPool>();
    private Dictionary<int, List<LevelOfComplexityOfBehavior>> _enemieBehavior;

    private bool _isWaveIsOver { get { return (_numberOfEnemiesInWave <= 0 && _enemiesInGame.Count <= 0); } }
    private int _numberOfEnemiesInWave, _numberWave;
    private void Start()
    {
        PoolEnemy.Instance.EnemyReturnToPool += DeathOfTheEnemy;
        _targetEnemy = FindObjectOfType<PlayerLife>().transform;
        GameStageEvent.StartLevel += StartWar;
    }
    private void FixedUpdate()
    {
        if (_enemiesInGame.Count < _attackWave.MaximNumumberOfLivingEnemy)
        {
            List<DisembarkationPoint> points = new List<DisembarkationPoint>();
            points.AddRange(_disembarkationPoints);
            while (true)
            {
                DisembarkationPoint disembarkationPoint = points[Random.Range(0, points.Count)];
                if (disembarkationPoint.CheckFree())
                {

                    ActivationEnemy(disembarkationPoint);
                    break;
                }
                else
                {

                    points.Remove(disembarkationPoint);
                }
                if (points.Count <= 0) break;
            }

        }
        if (_isWaveIsOver)
        {
            if (_numberWave < _attackWaves.Length - 1)
            {
                _numberWave++;
                ActivationNextWave();
            }
        }

    }
    private void OnValidate()
    {
        if (_attackWaves != null && _attackWaves.Length > 0)
        {
            for (int i = 0; i < _attackWaves.Length; i++)
            {
                _attackWaves[i].Name = "Wave " + (i + 1);
            }
        }
    }
    private void StartWar()
    {
        GameStageEvent.StartLevel -= StartWar;

        ActivationNextWave();
        foreach (var item in _disembarkationPoints)
        {
            if (_enemiesInGame.Count < _attackWave.MaximNumumberOfLivingEnemy)
            {
                ActivationEnemy(item);
            }
            else
                break;
        }
    }
    private void ActivationNextWave()
    {
        _numberOfEnemiesInWave = 0;
        _enemieBehavior = new Dictionary<int, List<LevelOfComplexityOfBehavior>>();
        for (int j = 0; j < _attackWave.Charatristic.Count; j++)
        {
            _numberOfEnemiesInWave += _attackWave.Charatristic[j].CountEnemy;
            _enemieBehavior[_attackWave.Charatristic[j].Enemy.EnemyID()] = new List<LevelOfComplexityOfBehavior>();

            for (int i = 0; i < _attackWave.Charatristic[j].Easy; i++)
                _enemieBehavior[_attackWave.Charatristic[j].Enemy.EnemyID()].Add(LevelOfComplexityOfBehavior.Easy);

            for (int i = 0; i < _attackWave.Charatristic[j].Normal; i++)
                _enemieBehavior[_attackWave.Charatristic[j].Enemy.EnemyID()].Add(LevelOfComplexityOfBehavior.Normal);

            for (int i = 0; i < _attackWave.Charatristic[j].Hard; i++)
                _enemieBehavior[_attackWave.Charatristic[j].Enemy.EnemyID()].Add(LevelOfComplexityOfBehavior.Hard);

            for (int i = 0; i < _attackWave.Charatristic[j].Random; i++)
                _enemieBehavior[_attackWave.Charatristic[j].Enemy.EnemyID()].Add(LevelOfComplexityOfBehavior.Random);
        }
    }
    private void ActivationEnemy(DisembarkationPoint disembarkationPoints)
    {
        if (_numberOfEnemiesInWave > 0)
        {
            List<int> enemyID = new List<int>();
            foreach (var item in _enemieBehavior)
            {
                if (item.Value.Count > 0)
                    enemyID.Add(item.Key);
            }

            int ID = enemyID[Random.Range(0, enemyID.Count)];
            IEnemuPool enemu = PoolEnemy.Instance.GetEnemy(ID, disembarkationPoints.transform.position, disembarkationPoints.transform.rotation);
            LevelOfComplexityOfBehavior behavior = _enemieBehavior[ID][Random.Range(0, _enemieBehavior[ID].Count)];
            _enemieBehavior[ID].Remove(behavior);
            enemu.Activation(_targetEnemy, behavior);
            _enemiesInGame.Add(enemu);
            _numberOfEnemiesInWave--;
        }
    }
    private void DeathOfTheEnemy(IEnemuPool enemu)
    {
        _enemiesInGame.Remove(enemu);
    }
    public int GetMaxActivatedEnemy()
    {
        int ActivatedEnemy = 0;
        foreach (var item in _attackWaves)
        {
            if (item.MaximNumumberOfLivingEnemy > ActivatedEnemy)
                ActivatedEnemy = item.MaximNumumberOfLivingEnemy;
        }
        return ActivatedEnemy;
    }
    public void AddEnemyVariations(EnemyLife[] enemies)
    {
        for (int i = 0; i < _attackWaves.Length; i++)
        {
            List<SpawnCharatristic> RemoveSpawn = new List<SpawnCharatristic>();
            RemoveSpawn.AddRange(_attackWaves[i].Charatristic);
            List<EnemyLife> AddEnemy = new List<EnemyLife>();
            AddEnemy.AddRange(enemies);

            for (int j = 0; j < _attackWaves[i].Charatristic.Count; j++)
            {
                for (int k = 0; k < enemies.Length; k++)
                {
                    if (_attackWaves[i].Charatristic[j].Enemy == enemies[k])
                    {
                        RemoveSpawn.Remove(_attackWaves[i].Charatristic[j]);
                        AddEnemy.Remove(enemies[k]);
                    }
                }
            }

            foreach (var item in RemoveSpawn)
            {
                _attackWaves[i].Charatristic.Remove(item);
            }
            foreach (var item in AddEnemy)
            {
                _attackWaves[i].AddSpawn(item);
            }
        }
    }

}

