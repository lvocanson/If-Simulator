using UnityEngine;

[System.Serializable]
public class KeyboardShortcut
{
    private readonly KeyCode _keyCode;
    private readonly bool _ctrl;
    private readonly bool _shift;
    private readonly bool _alt;

    public KeyboardShortcut(KeyCode keyCode, bool ctrl = false, bool shift = false, bool alt = false)
    {
        _keyCode = keyCode;
        _ctrl = ctrl;
        _shift = shift;
        _alt = alt;
    }

    public bool IsPressed(Event e)
    {
        return e.keyCode == _keyCode && e.control == _ctrl && e.shift == _shift && e.alt == _alt;
    }

    public override string ToString()
    {
        string str = "";
        if (_ctrl) str += "Ctrl+";
        if (_shift) str += "Shift+";
        if (_alt) str += "Alt+";
        str += _keyCode.ToString();
        return str;
    }
}
