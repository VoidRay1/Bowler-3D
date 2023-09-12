using System;
using UnityEngine;

[CreateAssetMenu]
public class DestroyObjectsInTimeAchievement : Achievement
{
    [SerializeField] private int _objectsToDestroy;
    [SerializeField] private float _timeToDestroy;

    [NonSerialized] private int _objectsDestroyedChain = 0;
    [NonSerialized] private float _timeAfterFirstObjectDestroyed = 0;

    private void OnEnable()
    {
#if UNITY_EDITOR
        TryGenerateGUID();
#endif
    }

    public void CompareDestroyObjectConditions()
    {
        if (State != AchievementState.AchievementNotReceived)
        {
            return;
        }

        if(_objectsDestroyedChain == 0)
        {
            _timeAfterFirstObjectDestroyed = Time.time;
        }

        _objectsDestroyedChain++;

        if (Time.time >= _timeAfterFirstObjectDestroyed + _timeToDestroy)
        {
            _objectsDestroyedChain = 0;
            return;
        }

        if (_objectsDestroyedChain >= _objectsToDestroy)
        {
            Debug.Log($"Destroy objects {_objectsToDestroy} in time {_timeToDestroy} achievement received");
            State = AchievementState.AchievementReceived;
            Reward.Data.IsUnlocked = true;
            OnAchievementReceived?.Invoke(this);
        }
    }
}
