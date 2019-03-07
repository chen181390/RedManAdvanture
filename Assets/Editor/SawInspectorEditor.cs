using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Saw))]
public class SawInspectorEditor : Editor
{
    private SerializedObject saw;

    private SerializedProperty sawType;
    private SerializedProperty routePoints;
    private SerializedProperty speed;

    void OnEnable()
    {
        this.saw = new SerializedObject(target);

        this.sawType = this.saw.FindProperty("sawType");
        this.routePoints = this.saw.FindProperty("routePoints");
        this.speed = this.saw.FindProperty("speed");
    }

    public override void OnInspectorGUI()
    {
        this.saw.Update();

        EditorGUILayout.PropertyField(this.sawType);

        switch((SawType)this.sawType.enumValueIndex)
        {
            case SawType.DirectByRoute:
                EditorGUILayout.PropertyField(this.routePoints);
                EditorGUILayout.PropertyField(this.speed);
                break;
        }

        this.saw.ApplyModifiedProperties();
    }
}
