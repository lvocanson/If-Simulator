using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BTreeEditorWindow : EditorWindow
{
    [SerializeField, Tooltip("The VisualTreeAsset (UXML) that defines the UI of the window")]
    private VisualTreeAsset _visualTreeAsset = default;

    [SerializeField, Tooltip("The StyleSheet (USS) that defines the UI of the window")]
    private StyleSheet _styleSheet = default;

    [MenuItem("CUSTOM AIs/Behavior Tree Editor")]
    public static void Open()
    {
        BTreeEditorWindow wnd = GetWindow<BTreeEditorWindow>();
        wnd.titleContent = new GUIContent("BTreeEditorWindow");
    }

    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        root.styleSheets.Add(_styleSheet);
        _visualTreeAsset.CloneTree(root);
    }
}
