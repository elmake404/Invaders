using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelOfComplexityOfBehavior
{
    Easy, Normal, Hard, Random
}
[System.Serializable]
public struct ComplexityOfBehavior
{
    [HideInInspector]
    public string Name;
    [HideInInspector]
    public LevelOfComplexityOfBehavior LevelOfComplexityOfBehavior;

    public Field[] Field;
    public int amontOfPoints;
    public float maxDistanceInOneStepForvard, maxDistanceInOneStepBeck;
}
public class Battlefield : MonoBehaviour
{
    public static Battlefield Instens;
    [SerializeField]
    public ComplexityOfBehavior[] _complexityOfBehavior;
    private Dictionary<LevelOfComplexityOfBehavior, ComplexityOfBehavior> _dictionaryFieldsComplexity = new Dictionary<LevelOfComplexityOfBehavior, ComplexityOfBehavior>();
    private void Awake()
    {
        for (int i = 0; i < _complexityOfBehavior.Length; i++)
        {

            _dictionaryFieldsComplexity[_complexityOfBehavior[i].LevelOfComplexityOfBehavior]
                = _complexityOfBehavior[i];
        }

        Instens = this;
    }
    public Vector3[] GetPointsOnBattelefield(LevelOfComplexityOfBehavior levelOfComplexityOfBehavior, Vector3 position,bool reCall)
    {
        ComplexityOfBehavior complexity = _dictionaryFieldsComplexity[levelOfComplexityOfBehavior];
        int Lenght = reCall ? complexity.amontOfPoints + 1 : complexity.amontOfPoints;
        Vector3[] points = new Vector3[Lenght];
        Vector3 oldPos = position;

        for (int i = 0; i < points.Length; i++)
        {
            if (reCall)
            {
                reCall = false;
                points[i] = complexity.Field[Random.Range(0, complexity.Field.Length)].GetPointBeck(position, complexity.maxDistanceInOneStepBeck, true);
                oldPos = points[i];
            }
            else
            {
                points[i] = complexity.Field[Random.Range(0, complexity.Field.Length)].GetPoint(oldPos, complexity.maxDistanceInOneStepForvard, Side.Length, true);
                oldPos = points[i];
            }
        }
        return points;
    }
    public Vector3 GetPointInTheOtherSide(LevelOfComplexityOfBehavior levelOfComplexityOfBehavior, Vector3 position, bool IsRight)
    {
        position = _dictionaryFieldsComplexity[levelOfComplexityOfBehavior].Field[0].GetPointOppositeSideAbscissa(position, IsRight);
        return position;
    }
    private void OnValidate()
    {
        if (_complexityOfBehavior.Length == 0)
        {
            _complexityOfBehavior = new ComplexityOfBehavior[4];

            _complexityOfBehavior[0].Name = "Easy";
            _complexityOfBehavior[0].LevelOfComplexityOfBehavior = LevelOfComplexityOfBehavior.Easy;

            _complexityOfBehavior[1].Name = "Normal";
            _complexityOfBehavior[1].LevelOfComplexityOfBehavior = LevelOfComplexityOfBehavior.Normal;

            _complexityOfBehavior[2].Name = "Hard";
            _complexityOfBehavior[2].LevelOfComplexityOfBehavior = LevelOfComplexityOfBehavior.Hard;

            _complexityOfBehavior[3].Name = "Random";
            _complexityOfBehavior[3].LevelOfComplexityOfBehavior = LevelOfComplexityOfBehavior.Random;
        }
    }
}
