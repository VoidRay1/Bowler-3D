using UnityEngine;

[CreateAssetMenu]
public class LevelAchievement : Achievement
{
    [SerializeField] private int _levelToAchieve;

    private void OnEnable()
    {
#if UNITY_EDITOR
        TryGenerateGUID();
#endif
    }

    public void CompareLevel(int level)
    {
        if (State != AchievementState.AchievementNotReceived)
        {
            return;
        }
        if(_levelToAchieve == level)
        {
            Debug.Log($"Level {_levelToAchieve} achievement received");
            State = AchievementState.AchievementReceived;
            Reward.Data.IsUnlocked = true;
            OnAchievementReceived?.Invoke(this);
        }
    }
}