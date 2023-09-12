using UnityEngine;

public class WaitForPressState : BallBaseState
{
    private float _force;
    private PlayerInputs.BallBehaviourActionsActions BallBehaviourActions => InputActions.Instance.Actions.BallBehaviourActions;

    public WaitForPressState(float force, Rigidbody rigidbody, IBallStateSwitcher stateSwitcher) 
        : base(rigidbody, stateSwitcher)
    {
        _force = force;
    }

    public override void Start()
    {
        Rigidbody.isKinematic = true;
    }

    public override void Update()
    {
        if (BallBehaviourActions.BallAxisY.WasPressedThisFrame())
        {
            Rigidbody.isKinematic = false;
            float xAxis = BallBehaviourActions.BallAxisX.ReadValue<float>();
            float yAxis = BallBehaviourActions.BallAxisY.ReadValue<float>();
            Rigidbody.AddForce(new Vector3(xAxis, 0.0f, yAxis) * _force, ForceMode.Impulse);
            StateSwitcher.SwitchState<MotionState>();
        }
    }
}