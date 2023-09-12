using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private ScoreDisplayer _scoreDisplayer;
    [SerializeField] private float _timeToDecreaseMultiplier;
    [SerializeField] private float _stepTimeMultiplierDecrease;
    [SerializeField] private float _stepMultiplierDecrease;
    [SerializeField, Range(1.0f, 1.5f)] private float _minScoreMultiplier;
    [SerializeField, Range(2.0f, 10.0f)] private float _maxScoreMultiplier;
    [SerializeField, Range(0.1f, 0.5f)] private float _minTimeToFillScore;
    [SerializeField, Range(0.5f, 5.0f)] private float _maxTimeToFillScore;

    private int _currentScore;
    private float _currentScoreMultiplier;
    private float _currentTime;
    private float _currentTimeStepToDecrease;
    private readonly Queue<IEnumerator> _scoreLerpCoroutines = new Queue<IEnumerator>();

    private bool IsPaused => GameContext.Instance.PauseManager.IsPaused;

    public int CurrentScore => _currentScore;
    [HideInInspector] public static UnityEvent<int> OnScoreUpdated = new UnityEvent<int>();

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        _currentScore = 0;
        _currentScoreMultiplier = _minScoreMultiplier;
        _currentTime = _timeToDecreaseMultiplier;
        _currentTimeStepToDecrease = _stepMultiplierDecrease;
        _scoreDisplayer.DisplayScore(_currentScore);
        _scoreDisplayer.DisplayScoreMultiplier(_currentScoreMultiplier);
    }

    public void SelfUpdate()
    {
        if (IsPaused)
        {
            return;
        }
        _currentTime -= Time.deltaTime;
        TryDecreaseMultiplier();
    }


    public void AddScore(int score)
    {
        if(score <= 0)
        {
            return;
        }

        float startScoreValue = _currentScore;
        float endScoreValue = (float)(_currentScore + (score * Math.Round(_currentScoreMultiplier, 2)));
        float normalizedValue = startScoreValue / endScoreValue;
        float duration = Mathf.Lerp(_minTimeToFillScore, _maxTimeToFillScore, normalizedValue);

        _scoreLerpCoroutines.Enqueue(LerpScore(startScoreValue, endScoreValue, duration));
        if(_scoreLerpCoroutines.Count == 1)
        {
            StartCoroutine(StartCoroutinesQueue());
        }

        _currentScore += (int)Math.Round((score * Math.Round(_currentScoreMultiplier, 2)));
        OnScoreUpdated?.Invoke(_currentScore);
    }

    private IEnumerator StartCoroutinesQueue()
    {
        while(_scoreLerpCoroutines.Count > 0)
        {
            IEnumerator currentLerpCoroutine = _scoreLerpCoroutines.Peek();
            yield return StartCoroutine(currentLerpCoroutine);
            _scoreLerpCoroutines.Dequeue();
        }
    }

    private IEnumerator LerpScore(float startScoreValue, float endScoreValue, float duration)
    {
        float elapsedTime = 0.0f;
        float nextValue;
        while(elapsedTime < duration)
        {
            nextValue = Mathf.Lerp(startScoreValue, endScoreValue, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            _scoreDisplayer.DisplayScore((int)nextValue);
            yield return null;
        }
        _scoreDisplayer.DisplayScore((int)endScoreValue);
    }

    public void IncreaseMultiplier(float value)
    {
        if(_currentScoreMultiplier + value < _maxScoreMultiplier)
        {
            _currentScoreMultiplier += value;
        }
        else
        {
            _currentScoreMultiplier = _maxScoreMultiplier;
        }
        _currentTime = _timeToDecreaseMultiplier;
        _currentTimeStepToDecrease = _stepTimeMultiplierDecrease;
        _scoreDisplayer.DisplayScoreMultiplier(_currentScoreMultiplier);
    }

    private void TryDecreaseMultiplier()
    {
        if (Math.Round(_currentScoreMultiplier, 2) <= Math.Round(_minScoreMultiplier, 2))
        {
            _currentScoreMultiplier = _minScoreMultiplier;
            return;
        }
        if (_currentTime < 0)
        {
            _currentTimeStepToDecrease -= Time.deltaTime;
            if (_currentTimeStepToDecrease < 0)
            {
                _currentScoreMultiplier -= _stepMultiplierDecrease;
                _currentTimeStepToDecrease = _stepTimeMultiplierDecrease;
                _scoreDisplayer.DisplayScoreMultiplier(_currentScoreMultiplier);
            }
        }
    }
}