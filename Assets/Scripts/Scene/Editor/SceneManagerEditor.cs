using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ArenaManager))]
public class SceneManagerEditor : Editor
{
    private SceneAsset sceneAsset = null;

    private void OnEnable()
    {
        
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.BeginHorizontal();
        {
            sceneAsset = EditorGUILayout.ObjectField("Scene: ", sceneAsset, typeof(SceneAsset), false) as SceneAsset;
            if (GUILayout.Button("Add"))
                (target as ArenaManager).AddScene(sceneAsset.name);
        }
        GUILayout.EndHorizontal();
    }
}
