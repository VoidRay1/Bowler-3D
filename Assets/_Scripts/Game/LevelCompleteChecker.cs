using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelCompleteChecker : MonoBehaviour
{
    private List<DestructableObject> _destructableObjects;

    [HideInInspector] public UnityEvent OnLevelCompleted = new UnityEvent();
    [HideInInspector] public static UnityEvent OnDestructableObjectDestroyed = new UnityEvent();

    public void Init(List<DestructableObject> destructableObjects)
    {
        UnSubscribeOnDestructaleObjectsEvents();
        _destructableObjects = destructableObjects;
        SubscribeOnDestructaleObjectsEvents();
    }

    private void SubscribeOnDestructaleObjectsEvents()
    {
        if (_destructableObjects == null)
        {
            return;
        }
        foreach (DestructableObject destructableObject in _destructableObjects)
        {
            destructableObject.OnDestructableObjectDestroyed.AddListener(CheckLevelCompleteConditions);
        }
    }

    private void UnSubscribeOnDestructaleObjectsEvents()
    {
        if (_destructableObjects == null)
        {
            return;
        }
        foreach (DestructableObject destructableObject in _destructableObjects)
        {
            destructableObject.OnDestructableObjectDestroyed.RemoveListener(CheckLevelCompleteConditions);
        }
    }

    private void CheckLevelCompleteConditions(DestructableObject destructableObject)
    {
        _destructableObjects.Remove(destructableObject);
        if(_destructableObjects.Count == 0)
        {
            OnLevelCompleted?.Invoke();
        }
        OnDestructableObjectDestroyed?.Invoke();
    }
}