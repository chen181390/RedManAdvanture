using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShootMace))]
public class ShootMaceInspectorEditor : Editor
{
    private SerializedObject shootMace;

    private SerializedProperty awakeMoveTagret;
    private SerializedProperty awakeMoveSpeed;
    private SerializedProperty shootInterval;
    private SerializedProperty isTrace;
    private SerializedProperty traceLeftLimitX;
    private SerializedProperty traceRightLimitX;
    private SerializedProperty traceSpeed;

    void OnEnable()
    {
        this.shootMace = new SerializedObject(target);

        this.awakeMoveTagret = this.shootMace.FindProperty("awakeMoveTarget");
        this.awakeMoveSpeed = this.shootMace.FindProperty("awakeMoveSpeed");
        this.shootInterval = this.shootMace.FindProperty("shootInterval");
        this.isTrace = this.shootMace.FindProperty("isTrace");
        this.traceLeftLimitX = this.shootMace.FindProperty("traceLeftLimitX");
        this.traceRightLimitX = this.shootMace.FindProperty("traceRightLimitX");
        this.traceSpeed = this.shootMace.FindProperty("traceSpeed");
    }

    public override void OnInspectorGUI()
    {
        this.shootMace.Update();

        EditorGUILayout.PropertyField(this.awakeMoveTagret);
        EditorGUILayout.PropertyField(this.awakeMoveSpeed);
        EditorGUILayout.PropertyField(this.shootInterval);
        EditorGUILayout.PropertyField(this.isTrace);
        
        if (this.isTrace.boolValue)
        {
            EditorGUILayout.PropertyField(this.traceLeftLimitX);
            EditorGUILayout.PropertyField(this.traceRightLimitX);
            EditorGUILayout.PropertyField(this.traceSpeed);
        }

        this.shootMace.ApplyModifiedProperties();
    }
}
