using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An event that can be raised without any parameter.
/// </summary>
[CreateAssetMenu(fileName = "Void_Channel", menuName = "Scriptable Objects/Events/Void Event")]
public class VoidEvent : ScriptableObject
{
    private readonly List<VoidEventListener> _listeners = new();

    /// <summary>
    /// Raises the event. This will trigger all the listeners.
    /// </summary>
    public void Raise()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].OnEventRaised();
    }

    /// <summary>
    /// Registers a listener to the event.
    /// </summary>
    public void Register(VoidEventListener listener)
    {
        if (!_listeners.Contains(listener))
            _listeners.Add(listener);
    }

    /// <summary>
    /// Unregisters a listener from the event.
    /// </summary>
    public void Unregister(VoidEventListener listener)
    {
        if (_listeners.Contains(listener))
            _listeners.Remove(listener);
    }
}
