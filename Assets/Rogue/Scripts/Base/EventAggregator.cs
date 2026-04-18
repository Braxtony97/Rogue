using System;
using System.Collections.Generic;
using UnityEngine;

public class EventAggregator
{
    public static EventAggregator Instance => _instance ?? (_instance = new EventAggregator());

    private static EventAggregator _instance;
    private readonly Dictionary<string, List<Delegate>> _signalsCallbacks = new();

    public void Subscribe<T>(Action<T> callback)
    {
        string key = typeof(T).FullName;

        if (_signalsCallbacks.ContainsKey(key))
            _signalsCallbacks[key].Add(callback);
        else
            _signalsCallbacks.Add(key, new List<Delegate>() { callback });
    }

    public void Publish<T>(T signal)
    {
        string key = typeof(T).FullName;

        if (_signalsCallbacks.ContainsKey(key))
        {
            foreach (Delegate del in _signalsCallbacks[key])
            {
                Action<T> callback = del as Action<T>;
                callback?.Invoke(signal);
            }
        }
    }

    public void Unsubscribe<T>(Action<T> callback)
    {
        string key = typeof(T).FullName;

        if (_signalsCallbacks.ContainsKey(key))
            _signalsCallbacks[key].Remove(callback);
    }
}

