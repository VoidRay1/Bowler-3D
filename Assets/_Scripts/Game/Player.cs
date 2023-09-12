using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private BallsLeftDisplayer _attemptsDisplayer;
    private int _ballsLeft;

    public int BallsLeft => _ballsLeft;

    public void AddAttempts(int attempts)
    {
        if(attempts < 0)
        {
            return;
        }
        _ballsLeft += attempts;
        _attemptsDisplayer.DisplayBallsLeft(_ballsLeft);
    }

    public void DecreaseAttempts()
    {
        _ballsLeft--;
        _attemptsDisplayer.DisplayBallsLeft(_ballsLeft);
    }
}