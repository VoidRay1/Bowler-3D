using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplierHandler : MonoBehaviour
{
    [SerializeField] private ScoreSystem _scoreSystem;
    [SerializeField, Range(0.2f, 0.3f)] private float _baseScoreMultiplier;
    [SerializeField] private float _timeToDiscardDestractionMultiplierSequence;

    private List<DestructableObject> _destructableObjects;
    private float _currentTimeToDiscardDestractionMultiplierSequence;
    private int _destructableObjectsCountInSequence;

    private bool IsPaused => GameContext.Instance.PauseManager.IsPaused;

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        _currentTimeToDiscardDestractionMultiplierSequence = _timeToDiscardDestractionMultiplierSequence;
        _destructableObjectsCountInSequence = 0;
    }

    public void SelfUpdate()
    {
        if (IsPaused)
        {
            return;
        }
        _currentTimeToDiscardDestractionMultiplierSequence -= Time.deltaTime;
        TryDiscardDestractionMultiplierSequence();
    }

    public void Init(List<DestructableObject> destructableObjects)
    {
        UnSubscribeOnDestructaleObjectsEvents();
        _destructableObjects = destructableObjects;
        SubscribeOnDestructaleObjectsEvents();
    }

    private void SubscribeOnDestructaleObjectsEvents()
    {
        if(_destructableObjects == null)
        {
            return;
        }
        foreach(DestructableObject destructableObject in _destructableObjects)
        {
            destructableObject.OnDestructableObjectDestroyed.AddListener(CalculateScoreMultiplier);
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
            destructableObject.OnDestructableObjectDestroyed.RemoveListener(CalculateScoreMultiplier);
        }
    }

    private void TryDiscardDestractionMultiplierSequence()
    {
        if(_destructableObjectsCountInSequence == 0)
        {
            _currentTimeToDiscardDestractionMultiplierSequence = _timeToDiscardDestractionMultiplierSequence;
            return;
        }
        if(_currentTimeToDiscardDestractionMultiplierSequence < 0)
        {
            _destructableObjectsCountInSequence--;
            _currentTimeToDiscardDestractionMultiplierSequence = _timeToDiscardDestractionMultiplierSequence;
        }
    }

    private void CalculateScoreMultiplier(DestructableObject destructableObject)
    {
        _destructableObjects.Remove(destructableObject);
        _destructableObjectsCountInSequence++;
        _currentTimeToDiscardDestractionMultiplierSequence = _timeToDiscardDestractionMultiplierSequence;
        _scoreSystem.IncreaseMultiplier(_destructableObjectsCountInSequence * _baseScoreMultiplier);
        _scoreSystem.AddScore(destructableObject.DestructionReward);   
    }
}