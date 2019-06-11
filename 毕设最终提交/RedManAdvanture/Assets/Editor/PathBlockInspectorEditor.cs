using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathBlock))]
public class PathBlockInspectorEditor : Editor
{
    
    private SerializedObject pathBlock;

    private SerializedProperty behaviourType;
    private SerializedProperty routePoints;
    private SerializedProperty moveSpeed;
    private SerializedProperty isMoveOnce;
    private SerializedProperty dropGravity;
    private SerializedProperty meteorPrefab;
    private SerializedProperty meteorPos;
    private SerializedProperty meteorImpulse;

    void OnEnable()
    {
       this.pathBlock = new SerializedObject(target);

       this.behaviourType = this.pathBlock.FindProperty("behaviourType");
       this.routePoints = this.pathBlock.FindProperty("routePoints");
       this.moveSpeed = this.pathBlock.FindProperty("moveSpeed");
       this.isMoveOnce = this.pathBlock.FindProperty("isMoveOnce");
       this.dropGravity = this.pathBlock.FindProperty("dropGravity");
       this.meteorPrefab = this.pathBlock.FindProperty("meteorPrefab");
       this.meteorPos = this.pathBlock.FindProperty("meteorPos");
       this.meteorImpulse = this.pathBlock.FindProperty("meteorImpulse");
    }

    public override void OnInspectorGUI()
    {
        this.pathBlock.Update();

        EditorGUILayout.PropertyField(this.behaviourType);

        switch((PathBlockBehaviourType)this.behaviourType.enumValueIndex)
        {
            case PathBlockBehaviourType.DirectByRoute:
            case PathBlockBehaviourType.TriggerThenDirectByRoute:
                EditorGUILayout.PropertyField(this.routePoints, true);
                EditorGUILayout.PropertyField(this.moveSpeed);
                EditorGUILayout.PropertyField(this.isMoveOnce);
                break;

            case PathBlockBehaviourType.Drop:
                EditorGUILayout.PropertyField(this.dropGravity);
                break;

            case PathBlockBehaviourType.CallMeteor:
                EditorGUILayout.PropertyField(this.meteorPrefab);
                EditorGUILayout.PropertyField(this.meteorPos);
                EditorGUILayout.PropertyField(this.meteorImpulse);
                break;
        }

        this.pathBlock.ApplyModifiedProperties();
    }
}
