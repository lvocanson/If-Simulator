using UnityEditor;
using UnityEditor.Callbacks;
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
        private readonly KeyboardShortcut _renameShortcut = new(KeyCode.F2);
        private readonly KeyboardShortcut _duplicateShortcut = new(KeyCode.D, true);

        [MenuItem("CUSTOM AIs/BTreeEditorWindow")]
        public static void Open()
        {
            BTreeEditorWindow wnd = GetWindow<BTreeEditorWindow>();
            wnd.titleContent = new GUIContent("BTreeEditorWindow");
        }

        public static void Open(BTreeSo tree)
        {
            BTreeEditorWindow wnd = GetWindow<BTreeEditorWindow>();
            wnd.titleContent = new GUIContent("BTreeEditorWindow");
            wnd._treeView.Open(tree);
        }

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceID, int _)
        {
            if (EditorUtility.InstanceIDToObject(instanceID) is BTreeSo tree)
            {
                Open(tree);
                return true;
            }
            return false;
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
                else if (_renameShortcut.IsPressed(e))
                {
                    _treeView.RenameSelection();
                    e.Use();
                }
                else if (_duplicateShortcut.IsPressed(e))
                {
                    _treeView.RemoveFromSelection(_treeView.RootNodeView);
                    _treeView.DuplicateSelection();
                    e.Use();
                }
            }
            
            if (_treeView?.RenamePending == true)
            {
                _treeView.RenameSelection();
            }
        }

        // Called when the selection in the project window changes.
        private void OnSelectionChange()
        {
            // Make sure the asset is persistant.
            if (Selection.activeObject is BTreeSo tree && AssetDatabase.IsMainAsset(tree))
            {
                _treeView.Open(tree);
            }
        }
    }
}
