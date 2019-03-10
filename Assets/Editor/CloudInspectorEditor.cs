using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cloud))]
public class CloudInspectorEditor : Editor
{
    private SerializedObject cloud;

    private SerializedProperty type;
    private SerializedProperty meteorPrefab;
    private SerializedProperty meteorPos;
    private SerializedProperty meteorImpulse;
    private SerializedProperty routePoints;
    private SerializedProperty moveSpeed;

    void OnEnable()
    {
        this.cloud = new SerializedObject(target);

        this.type = this.cloud.FindProperty("type");
        this.meteorPrefab = this.cloud.FindProperty("meteorPrefab");
        this.meteorPos = this.cloud.FindProperty("meteorPos");
        this.meteorImpulse = this.cloud.FindProperty("meteorImpulse");
        this.routePoints = this.cloud.FindProperty("routePoints");
        this.moveSpeed = this.cloud.FindProperty("moveSpeed");
    }

    public override void OnInspectorGUI()
    {
        this.cloud.Update();

        EditorGUILayout.PropertyField(this.type);

        switch((CloudType)this.type.enumValueIndex)
        {
            case CloudType.CallMeteor:
                EditorGUILayout.PropertyField(this.meteorPrefab);
                EditorGUILayout.PropertyField(this.meteorPos);
                EditorGUILayout.PropertyField(this.meteorImpulse);
                break;

            case CloudType.DirectByRoute:
            case CloudType.TriggerThenDirectByRoute:
                EditorGUILayout.PropertyField(this.routePoints, true);
                EditorGUILayout.PropertyField(this.moveSpeed);
                break;
        }

        this.cloud.ApplyModifiedProperties();
    }
}
