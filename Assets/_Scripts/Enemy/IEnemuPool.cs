using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemuPool 
{
    void Activation(Transform target, LevelOfComplexityOfBehavior slevelOfComplexityOfBehavior);
    int EnemyID();
    GameObject GetObject();
}
