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
    private SerializedProperty dropGravity;

    void OnEnable()
    {
       this.pathBlock = new SerializedObject(target);

       this.behaviourType = this.pathBlock.FindProperty("behaviourType");
       this.routePoints = this.pathBlock.FindProperty("routePoints");
       this.moveSpeed = this.pathBlock.FindProperty("moveSpeed");
       this.dropGravity = this.pathBlock.FindProperty("dropGravity");
    }

    public override void OnInspectorGUI()
    {
        this.pathBlock.Update();

        EditorGUILayout.PropertyField(this.behaviourType);

        switch((PathBlockBehaviourType)this.behaviourType.enumValueIndex)
        {
            case PathBlockBehaviourType.DirectByRoute:
            case PathBlockBehaviourType.TriggerThenDirectByRoute:
                EditorGUILayout.PropertyField(this.routePoints);
                EditorGUILayout.PropertyField(this.moveSpeed);
                break;

            case PathBlockBehaviourType.Drop:
                EditorGUILayout.PropertyField(this.dropGravity);
                break;
        }

        this.pathBlock.ApplyModifiedProperties();
    }
}
