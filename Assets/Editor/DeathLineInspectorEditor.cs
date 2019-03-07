using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DeathLine))]
public class DeathLineInspectorEditor : Editor
{
    private SerializedObject deathLine;

    private SerializedProperty deathLineType;
    private SerializedProperty touchBlock;
    private SerializedProperty fadeInSpeed;
    private SerializedProperty nextScene;

void OnEnable()
{
    this.deathLine = new SerializedObject(target);
    
    this.deathLineType = this.deathLine.FindProperty("deathLineType");
    this.touchBlock = this.deathLine.FindProperty("touchBlock");
    this.fadeInSpeed = this.deathLine.FindProperty("fadeInSpeed");
    this.nextScene = this.deathLine.FindProperty("nextScene");
}

public override void OnInspectorGUI()
{
    this.deathLine.Update();

    EditorGUILayout.PropertyField(this.deathLineType);

    switch((DeathLineType)this.deathLineType.enumValueIndex)
    {
        case DeathLineType.Right:
            EditorGUILayout.PropertyField(this.touchBlock);
            EditorGUILayout.PropertyField(this.fadeInSpeed);
            EditorGUILayout.PropertyField(this.nextScene);
            break;
    }

    this.deathLine.ApplyModifiedProperties();
}

}
