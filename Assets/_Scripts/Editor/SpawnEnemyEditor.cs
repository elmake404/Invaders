using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpawnEnemy))]
public class SpawnEnemyEditor : Editor
{
    //public override void OnInspectorGUI()
    //{
    //    DrawDefaultInspector();

    //    SpawnEnemy spawn = (SpawnEnemy) target;
    //    serializedObject.Update();
    //    SerializedProperty elementProp = serializedObject.FindProperty("_attackWaves");
    //    spawn.DisplayArray(elementProp);
    //}
}
