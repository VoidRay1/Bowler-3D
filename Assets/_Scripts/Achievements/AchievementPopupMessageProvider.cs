using System.Collections.Generic;
using UnityEngine;

public class AchievementPopupMessageProvider : AssetLoader
{
    private readonly Queue<Achievement> _achievements = new Queue<Achievement>();
    private AchievementPopupMessage _currentDisplayedAchievementPopupMessage;

    public void TryShowPopupMessage(Achievement achievement)
    {
        _achievements.Enqueue(achievement);
        if (_achievements.Count == 1)
        {
            LoadAchievementPopupMessage(achievement);
        }
    }

    private void AchievementPopupMessageHide()
    {
        Unload(_currentDisplayedAchievementPopupMessage.gameObject);
        _achievements.Dequeue();
        if (_achievements.TryPeek(out Achievement achievement))
        {
            LoadAchievementPopupMessage(achievement);
        }
    }

    private async void LoadAchievementPopupMessage(Achievement achievement)
    {
        AchievementPopupMessage achievementPopupMessage = await Load<AchievementPopupMessage>(Constants.AddressablesIds.ACHIEVEMENT_POPUP_MESSAGE);
        achievementPopupMessage.Init(AnchorPreset.TopCenter,
            new Vector3(0.0f, achievementPopupMessage.GetSizeDelta().y / 2, 0.0f),
            achievement.Description, AchievementPopupMessageHide);

        _currentDisplayedAchievementPopupMessage = achievementPopupMessage;
        _currentDisplayedAchievementPopupMessage.Show();
    }
}
