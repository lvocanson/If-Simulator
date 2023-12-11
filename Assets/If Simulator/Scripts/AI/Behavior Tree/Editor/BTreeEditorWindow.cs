using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BehaviorTree
{
    public class BTreeEditorWindow : EditorWindow
    {
        private BTreeAsset _tree = null;
        readonly FieldInfo _constructorField = typeof(BTreeAsset).GetField("_constructor", BindingFlags.NonPublic | BindingFlags.Instance);
        private BTreeAsset.NodeConstructor _constructor;

        public static void Open(BTreeAsset tree)
        {
            var window = GetWindow<BTreeEditorWindow>();
            window.titleContent = new GUIContent("Behavior Tree Editor: " + tree.name);
            window._tree = tree;
        }

        private void OnEnable()
        {
            _constructor = (BTreeAsset.NodeConstructor)_constructorField.GetValue(_tree);
        }

        private void OnGUI()
        {
            if (_tree == null)
            {
                EditorGUILayout.HelpBox("No tree asset assigned.", MessageType.Warning);
                return;
            }

            DrawToolbar();
        }

        private void DrawToolbar()
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
            if (GUILayout.Button("Select asset", EditorStyles.toolbarButton))
            {
                Selection.activeObject = _tree;
            }
            if (GUILayout.Button("Save asset", EditorStyles.toolbarButton))
            {
                _constructorField.SetValue(_tree, _constructor);
                EditorUtility.SetDirty(_tree);
                AssetDatabase.SaveAssets();
            }
            if (GUILayout.Button("Save and Close", EditorStyles.toolbarButton))
            {
                Close();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void OnDisable()
        {
            // Save the tree
            _constructorField.SetValue(_tree, _constructor);
            EditorUtility.SetDirty(_tree);
        }
    }
}
