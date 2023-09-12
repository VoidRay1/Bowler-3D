using UnityEngine;

public class SwipeAreaWindowProvider : MonoBehaviour
{
    [SerializeField] private SwipeAreaDetector _swipeDetector;
    [SerializeField] private AchievementsWindow _achievementsWindow;
    [SerializeField] private BallsCollectionWindow _ballsCollectionWindow;

    private void OnEnable()
    {
        _swipeDetector.OnSwipeAreaReachedMaxSize.AddListener(TryOpenWindow);
    }

    private void OnDisable()
    {
        _swipeDetector.OnSwipeAreaReachedMaxSize.RemoveListener(TryOpenWindow);
    }

    private void TryOpenWindow(SwipeWindowType swipeWindowType)
    {
        switch(swipeWindowType)
        {
            case SwipeWindowType.BallsCollectionWindow:
                _ballsCollectionWindow.ShowCollection(GameContext.Instance.BallsList.Balls, GameContext.Instance.SelectedBall);
                break;
            case SwipeWindowType.AchievementsWindow:
                _achievementsWindow.ShowAchievements(GameContext.Instance.AchievementsList.Achievements);
                break;
        }
    }
}