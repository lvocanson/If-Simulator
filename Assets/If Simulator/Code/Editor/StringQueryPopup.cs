using System;
using UnityEditor;
using UnityEngine;

public class StringQueryPopup : EditorWindow
{
    Action<string> _callback;
    string _message;
    string _text;

    public static void Create(string title, string message, Action<string> callback) => Create(title, message, "", callback);
    public static void Create(string title, string message, string placeholder, Action<string> callback)
    {
        var window = CreateInstance<StringQueryPopup>();
        window.titleContent = new GUIContent(title);
        window._callback = callback;
        window._message = message;
        window._text = placeholder;

        // Set the window size to the size of the message + field + button.
        var size = GUI.skin.label.CalcSize(new GUIContent(message));
        size.x = Mathf.Max(size.x + 20, 200);
        size.y += GUI.skin.textField.lineHeight + GUI.skin.button.lineHeight + 20;
        window.minSize = window.maxSize = size;
        window.ShowAuxWindow();
    }

    void OnGUI()
    {
        GUILayout.Label(_message);
        _text = GUILayout.TextField(_text);
        if (GUILayout.Button("OK"))
        {
            _callback(_text);
            Close();
        }
    }
}
