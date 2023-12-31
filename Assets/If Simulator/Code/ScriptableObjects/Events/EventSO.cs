using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleEvent", menuName = "Scriptable Objects/Events/Simple Event")]
public class EventSO : ScriptableObject
{
    public event Action Event;

    public void Invoke()
    {
        Event?.Invoke();
    }
}
