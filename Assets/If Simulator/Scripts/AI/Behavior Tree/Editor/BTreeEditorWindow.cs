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

        private BTreeView _treeView;

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
        }

        private void OnSelectionChange()
        {
            if (Selection.activeObject is BTree tree)
            {
                _treeView.Open(tree);
            }
        }
    }

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
}
