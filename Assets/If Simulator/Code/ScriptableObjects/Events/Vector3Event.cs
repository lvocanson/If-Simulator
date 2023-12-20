using System.Collections.Generic;
using UnityEngine;

public interface IVector3EventListener
{
    /// <summary>
    /// Called when a subscribed event is raised.
    /// </summary>
    void OnEventRaised(Vector3 value);
}

[CreateAssetMenu(fileName = "Vector3_Channel", menuName = "Scriptable Objects/Events/Vector3 Event")]
public class Vector3Event : ScriptableObject
{
    private readonly List<IVector3EventListener> _listeners = new();

    /// <summary>
    ///    /// Raises the event.
    /// </summary>
    public void Raise(Vector3 value)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].OnEventRaised(value);
    }

    /// <summary>
    /// Registers a listener to the event.
    /// </summary>
    public void Register(IVector3EventListener listener)
    {
        if (!_listeners.Contains(listener))
            _listeners.Add(listener);
    }

    /// <summary>
    /// Unregisters a listener from the event.
    /// </summary>
    public void Unregister(IVector3EventListener listener)
    {
        if (_listeners.Contains(listener))
            _listeners.Remove(listener);
    }
}
