using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BonusCharacteristics
{
    private bool[] _randomList;
    [HideInInspector]
    public string Name;
    public BonusMove Bonus;
    [SerializeField]
    [Range(0, 100)]
    private int _probability;
    public bool ProbabilityCheck()
    {
        if (_randomList==null)
        {
            Randomizer();
        }
        return _randomList[Random.Range(0, _randomList.Length)];
    }
    private void Randomizer()
    {
        bool[] randomList = new bool[100];
        int probability = _probability;
        while (probability > 0)
        {
            int i = Random.Range(0,randomList.Length);
            if (!randomList[i])
            {
                randomList[i] = true;
                probability--;
            }
        }
        _randomList = randomList;
    }
}
public class BonusSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPos;
    [SerializeField]
    private BonusCharacteristics[] _bonusSpawners;
    void Start()
    {
        GetComponent<HeathMeter>().Died += LuckTest;
    }

    private void LuckTest()
    {
        BonusCharacteristics bonus = _bonusSpawners[Random.Range(0, _bonusSpawners.Length)];
        if (bonus.ProbabilityCheck())
        {
           BonusMove bonusMove =  Instantiate(bonus.Bonus, _spawnPos.position, bonus.Bonus.transform.rotation);
            bonusMove.SetPositionAlongTheApplicateAxis(PlayerMove.Position.position.z);
        }
    }
    private void OnValidate()
    {
        if (_bonusSpawners.Length > 0)
        {
            for (int i = 0; i < _bonusSpawners.Length; i++)
            {
                if (_bonusSpawners[i].Bonus != null)
                {
                    _bonusSpawners[i].Name = _bonusSpawners[i].Bonus.name;

                }
            }
        }
    }
}
