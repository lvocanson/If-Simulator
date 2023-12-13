using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BehaviorTree
{
    public class BTreeEditorWindow : EditorWindow
    {
        private BTreeAsset _tree = null;
        private readonly FieldInfo _serializedTreeField = typeof(BTreeAsset).GetField("_serializedTree", BindingFlags.NonPublic | BindingFlags.Instance);
        private string _serializedTree;

        public static void Open(BTreeAsset tree)
        {
            var window = GetWindow<BTreeEditorWindow>();
            window.titleContent = new GUIContent("Behavior Tree Editor: " + tree.name);
            window._tree = tree;
        }

        // Called when the window is opened.
        private void OnEnable()
        {
            if (_tree == null)
            {
                return;
            }

            _serializedTree = (string)_serializedTreeField.GetValue(_tree);
        }

        private void OnGUI()
        {
            if (_tree == null)
            {
                EditorGUILayout.HelpBox("No tree asset assigned.", MessageType.Warning);
                return;
            }
            _serializedTree ??= (string)_serializedTreeField.GetValue(_tree);

            DrawToolbar();
            DrawGraph();
        }

        private void DrawToolbar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            if (GUILayout.Button("Select asset", EditorStyles.toolbarButton))
            {
                Selection.activeObject = _tree;
            }
            GUI.enabled = hasUnsavedChanges;
            if (GUILayout.Button("Save asset", EditorStyles.toolbarButton))
            {
                SaveChanges();
            }
            if (GUILayout.Button("Discard changes", EditorStyles.toolbarButton))
            {
                DiscardChanges();
            }
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
        }

        private void DrawGraph()
        {
            EditorGUI.BeginChangeCheck();

            // Write the string
            _serializedTree = EditorGUILayout.TextArea(_serializedTree);

            if (EditorGUI.EndChangeCheck())
            {
                hasUnsavedChanges = true;
            }
        }

        public override void SaveChanges()
        {
            _serializedTreeField.SetValue(_tree, _serializedTree);
            EditorUtility.SetDirty(_tree);
            AssetDatabase.SaveAssets();
            hasUnsavedChanges = false;
        }

        public override void DiscardChanges()
        {
            _serializedTree = null;
            hasUnsavedChanges = false;
        }
    }
}
