/*using UnityEngine;

public class LowSpeedState : BallBaseState
{
    public LowSpeedState(float minHighSpeedThresold, Rigidbody rigidbody, IBallStateSwitcher stateSwitcher) 
        : base(minHighSpeedThresold, rigidbody, stateSwitcher)
    {

    }

    public override void Start()
    {
        
    }

    public override void Update()
    {
        if(_rigidbody.velocity.magnitude > _minHighSpeedThreshold)
        {
            _stateSwitcher.SwitchState<HighSpeedState>();
        }
    }
}*/