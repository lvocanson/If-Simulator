using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BehaviorTree
{
    [CustomEditor(typeof(BTree))]
    public class BTreeEditor : Editor
    {
        readonly FieldInfo _treeAssetField = typeof(BTree).GetField("_treeAsset", BindingFlags.NonPublic | BindingFlags.Instance);

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var treeAsset = (BTreeAsset)_treeAssetField.GetValue(target);
            if (treeAsset == null)
            {
                EditorGUILayout.HelpBox("No tree asset assigned.", MessageType.Warning);
            }
            else if (GUILayout.Button("Open Editor"))
            {
                BTreeEditorWindow.Open(treeAsset);
            }
        }
    }

    [CustomEditor(typeof(BTreeAsset))]
    public class BTreeAssetEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open Editor"))
            {
                BTreeEditorWindow.Open((BTreeAsset)target);
            }
        }
    }
}
