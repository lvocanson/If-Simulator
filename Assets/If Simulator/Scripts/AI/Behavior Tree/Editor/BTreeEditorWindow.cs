using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BehaviorTree
{
    public class BTreeEditorWindow : EditorWindow
    {
        [SerializeField, Tooltip("The VisualTreeAsset (UXML) that defines the UI of the window")]
        private VisualTreeAsset _visualTreeAsset = default;

        [SerializeField, Tooltip("The StyleSheet (USS) that defines the UI of the window")]
        private StyleSheet _styleSheet = default;

        // The tree view that displays the tree.
        private BTreeView _treeView;

        // Keyboard shortcuts.
        private readonly KeyboardShortcut _deleteShortcut = new(KeyCode.Delete);
        private readonly KeyboardShortcut _duplicateShortcut = new(KeyCode.D, true);

        [MenuItem("CUSTOM AIs/BTreeEditorWindow")]
        public static void Open()
        {
            BTreeEditorWindow wnd = GetWindow<BTreeEditorWindow>();
            wnd.titleContent = new GUIContent("BTreeEditorWindow");
        }

        public static void Open(BTree tree)
        {
            BTreeEditorWindow wnd = GetWindow<BTreeEditorWindow>();
            wnd.titleContent = new GUIContent("BTreeEditorWindow");
            wnd._treeView.Open(tree);
        }

        private void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            root.styleSheets.Add(_styleSheet);
            _visualTreeAsset.CloneTree(root);

            _treeView = root.Q<BTreeView>();

            OnSelectionChange();
        }

        private void OnGUI()
        {
            // Handle keyboard shortcuts.
            var e = Event.current;
            if (e.type == EventType.KeyDown)
            {
                if (_deleteShortcut.IsPressed(e))
                {
                    _treeView.RemoveFromSelection(_treeView.RootNodeView);
                    _treeView.DeleteSelection();
                    e.Use();
                }
                else if (_duplicateShortcut.IsPressed(e))
                {
                    _treeView.RemoveFromSelection(_treeView.RootNodeView);
                    _treeView.DuplicateSelection();
                    e.Use();
                }
            }
        }

        // Called when the selection in the project window changes.
        private void OnSelectionChange()
        {
            // Make sure the asset is persistant.
            if (Selection.activeObject is BTree tree && AssetDatabase.IsMainAsset(tree))
            {
                _treeView.Open(tree);
            }
        }
    }

    #region Custom Inspectors

    [CustomEditor(typeof(BTree))]
    public class BTreeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Open Editor"))
            {
                BTreeEditorWindow.Open((BTree)target);
            }
        }
    }

    [CustomEditor(typeof(BTreeRunner))]
    public class BTreeRunnerEditor : Editor
    {
        private readonly FieldInfo _treeField = typeof(BTreeRunner).GetField("_tree", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BTree tree = (BTree)_treeField.GetValue(target);

            if (GUILayout.Button("Open Editor"))
            {
                BTreeEditorWindow.Open(tree);
            }
        }
    }

    #endregion
}
