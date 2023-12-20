using System.Collections.Generic;
using UnityEngine;

public enum CollisionType
{
    Enter,
    Exit,
    Stay
}

public interface ICollider2DEventListener
{
    /// <summary>
    /// Called when a subscribed event is raised.
    /// </summary>
    void OnEventRaised(Collision2D collision, CollisionType type);
}

[CreateAssetMenu(fileName = "Collision2D_Channel", menuName = "Scriptable Objects/Events/Collision2D Event")]
public class Collision2DEvent : ScriptableObject
{
    private readonly List<ICollider2DEventListener> _listeners = new();

    /// <summary>
    /// Raises the event.
    /// </summary>
    public void Raise(Collision2D collision, CollisionType type)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].OnEventRaised(collision, type);
    }

    /// <summary>
    /// Registers a listener to the event.
    /// </summary>
    public void Register(ICollider2DEventListener listener)
    {
        if (!_listeners.Contains(listener))
            _listeners.Add(listener);
    }

    /// <summary>
    /// Unregisters a listener from the event.
    /// </summary>
    public void Unregister(ICollider2DEventListener listener)
    {
        if (_listeners.Contains(listener))
            _listeners.Remove(listener);
    }
}
