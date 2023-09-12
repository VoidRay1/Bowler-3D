using UnityEngine;

public abstract class BallBaseState
{
    protected readonly Rigidbody Rigidbody;
    protected readonly IBallStateSwitcher StateSwitcher;

    protected BallBaseState(Rigidbody rigidbody, IBallStateSwitcher stateSwitcher)
    {
        Rigidbody = rigidbody;
        StateSwitcher = stateSwitcher;
    }

    public abstract void Start();
    public abstract void Update();
}