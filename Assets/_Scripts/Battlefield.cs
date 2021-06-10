using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelOfComplexityOfBehavior
{
    Easy,Normal,Hard
}
public class Battlefield : Field
{
    public static Battlefield Instens;
    private void Awake()
    {
        Instens = this;
    }
    public Vector3[] GetPointsOnBattelefield(int amontOfPoints,Vector3 position)
    {
        Vector3[] points = new Vector3[amontOfPoints];
        Vector3 oldPos = position;

        for (int i = 0; i < amontOfPoints; i++)
        {
            points[i] = GetPoint(oldPos,3f,Side.Length,true);
            oldPos = points[i];
        }
        return points;
    }
}
