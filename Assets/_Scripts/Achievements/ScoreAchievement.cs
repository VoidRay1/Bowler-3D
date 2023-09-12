using UnityEngine;

[CreateAssetMenu]
public class ScoreAchievement : Achievement
{
    [SerializeField] private int _minScoreToAchieve;

    private void OnEnable()
    {
#if UNITY_EDITOR
        TryGenerateGUID();
#endif
    }

    public void CompareScore(int score)
    {
        if (State != AchievementState.AchievementNotReceived)
        {
            return;
        }
        if (score >= _minScoreToAchieve)
        {
            Debug.Log($"Score {_minScoreToAchieve} achievement received");
            State = AchievementState.AchievementReceived;
            Reward.Data.IsUnlocked = true;
            OnAchievementReceived?.Invoke(this);
        }
    }
}