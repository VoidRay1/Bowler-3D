using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private RespawnBallUI _respawnBallUI;
    [SerializeField] private Transform _startBallPosition;

    private Ball _ballPrefab;
    private Ball _currentBall;

    private void Start()
    {
        _ballPrefab = GameContext.Instance.SelectedBall.Data.Ball;
    }

    public void SpawnBall()
    {
        if(_currentBall != null)
        {
            Destroy(_currentBall.gameObject);
        }
        _currentBall = Instantiate(_ballPrefab, _startBallPosition.position, Quaternion.identity);
        _currentBall.Init(_respawnBallUI);
    }
}