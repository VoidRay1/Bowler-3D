public interface IBallStateSwitcher 
{
    void SwitchState<T>() where T : BallBaseState;
}