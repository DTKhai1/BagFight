using System.Collections.Generic;
using UnityEngine;

public interface IUpdateObserver
{
    void ObservedUpdate();
}
public interface IFixedUpdateObserver
{
    void ObservedFixedUpdate();
}
public class UpdateManager : MonoBehaviour
{
    private static List<IUpdateObserver> _observers = new List<IUpdateObserver>();
    private static List<IUpdateObserver> _pendingObservers = new List<IUpdateObserver>();
    private static int _currentUpdateIndex;

    private static List<IFixedUpdateObserver> _fixedObservers = new List<IFixedUpdateObserver>();
    private static List<IFixedUpdateObserver> _pendingFixedObservers = new List<IFixedUpdateObserver>();
    private static int _currentFixedUpdateIndex;
    private void Awake()
    {
        _observers.Clear();
        _pendingObservers.Clear();
        _fixedObservers.Clear();
        _pendingFixedObservers.Clear();
    }
    private void Update()
    {
        for (_currentUpdateIndex = _observers.Count - 1; _currentUpdateIndex >= 0; _currentUpdateIndex--)
        {
            if (_observers[_currentUpdateIndex] != null)
                _observers[_currentUpdateIndex].ObservedUpdate();
        }
        _pendingObservers.RemoveAll(observer => observer == null);
        _observers.AddRange(_pendingObservers);
        _pendingObservers.Clear();
    }

    public static void RegisterUpdateObserver(IUpdateObserver observer)
    {
        _pendingObservers.Add(observer);
    }
    public static void UnregisterUpdateObserver(IUpdateObserver observer)
    {
        _observers.Remove(observer);
        _currentUpdateIndex--;
    }

    private void FixedUpdate()
    {
        for (_currentFixedUpdateIndex = _fixedObservers.Count - 1; _currentFixedUpdateIndex >= 0; _currentFixedUpdateIndex--)
        {
            if (_fixedObservers[_currentFixedUpdateIndex] != null)
                _fixedObservers[_currentFixedUpdateIndex].ObservedFixedUpdate();
        }
        _pendingFixedObservers.RemoveAll(observer => observer == null);
        _fixedObservers.AddRange(_pendingFixedObservers);
        _pendingFixedObservers.Clear();
    }
    public static void RegisterFixedUpdateObserver(IFixedUpdateObserver observer)
    {
        _pendingFixedObservers.Add(observer);
    }
    public static void UnregisterFixedUpdateObserver(IFixedUpdateObserver observer)
    {
        _fixedObservers.Remove(observer);
        _currentFixedUpdateIndex--;
    }
}