/*using UnityEngine;

public class HighSpeedState : BallBaseState
{
    public HighSpeedState(float minHighSpeedThresold, Rigidbody rigidbody, IBallStateSwitcher stateSwitcher) 
        : base(minHighSpeedThresold, rigidbody, stateSwitcher)
    {
        
    }

    public override void Start()
    {
       
    }

    public override void Update()
    {
        if(_rigidbody.velocity.magnitude < _minHighSpeedThreshold)
        {
            _stateSwitcher.SwitchState<LowSpeedState>();
        }
    }
}*/