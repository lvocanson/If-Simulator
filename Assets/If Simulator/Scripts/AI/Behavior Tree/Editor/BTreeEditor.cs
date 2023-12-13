using System.Reflection;
using System.Text;
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
        readonly FieldInfo _serializedTreeField = typeof(BTreeAsset).GetField("_serializedTree", BindingFlags.NonPublic | BindingFlags.Instance);

        public override void OnInspectorGUI()
        {
            var asset = (BTreeAsset)target;
            if (GUILayout.Button("Open Editor"))
            {
                BTreeEditorWindow.Open(asset);
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Tree preview", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(GetPreview(asset), MessageType.None);
        }

        private string GetPreview(BTreeAsset asset)
        {
            string serializedTree = (string)_serializedTreeField.GetValue(asset);
            if (string.IsNullOrEmpty(serializedTree))
            {
                return "Tree is empty.";
            }

            var builder = new StringBuilder();
            int indent = 0;
            for (int i = 0; i < serializedTree.Length; i++)
            {
                char c = serializedTree[i];
                switch (c)
                {
                    case '{':
                        builder.Append('\n');
                        builder.Append(' ', indent);
                        builder.Append(c);
                        indent += 4;
                        builder.Append('\n');
                        builder.Append(' ', indent);
                        break;
                    case '}':
                        indent -= 4;
                        builder.Append('\n');
                        builder.Append(' ', indent);
                        builder.Append(c);
                        break;
                    case ',':
                        builder.Append(c);
                        builder.Append('\n');
                        builder.Append(' ', indent);
                        break;
                    default:
                        builder.Append(c);
                        break;
                }
            }
            return builder.ToString();
        }
    }
}
