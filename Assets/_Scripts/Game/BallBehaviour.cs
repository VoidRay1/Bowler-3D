using System;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour, IBallStateSwitcher, IPauseHandler
{
    [SerializeField] private float _force;
    [SerializeField] private float _minHighSpeedThresold;

    private Rigidbody _rigidbody;
    private RespawnBallUI _respawnBallUI;
    private BallBaseState _currentState;
    private Dictionary<Type, BallBaseState> _allStates;
    private Vector3 _velocityTemp;

    private bool IsPaused => GameContext.Instance.PauseManager.IsPaused;


    private void Update()
    {
        if (IsPaused)
        {
            return;
        }
        _currentState.Update();
    }

    public void Init(RespawnBallUI respawnBallUI, Rigidbody rigidbody)
    {
        _respawnBallUI = respawnBallUI;
        _rigidbody = rigidbody;

        GameContext.Instance.PauseManager.RegisterPauseListener(this);

        _allStates = new Dictionary<Type, BallBaseState>()
        {
            [typeof(WaitForPressState)] = new WaitForPressState(_force, _rigidbody, this),
            [typeof(MotionState)] = new MotionState(_rigidbody, this)
        };
        SwitchState<WaitForPressState>();
    }

    public void SwitchState<T>() where T : BallBaseState
    {
        _currentState = _allStates[typeof(T)];
        _currentState.Start();
        _respawnBallUI.BallStateChanged(_currentState);
    }

    public void SetPause(bool isPaused)
    {
        if (isPaused)
        {
            _velocityTemp = _rigidbody.velocity;
            _rigidbody.isKinematic = true;
        }
        else
        {
            if(_currentState is not WaitForPressState)
            {
                _rigidbody.isKinematic = false;
                _rigidbody.velocity = _velocityTemp;
            }
        }
    }

    private void OnDestroy()
    {
        GameContext.Instance.PauseManager.UnRegisterPauseListener(this);
    }
}