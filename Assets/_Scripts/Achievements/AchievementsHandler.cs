using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AchievementsHandler : AssetLoader
{
    private readonly AchievementsList _achievementsList;
    private readonly AchievementPopupMessageProvider _achievementPopupMessageProvider;

    public AchievementsHandler(AchievementsList achievementsList, AchievementPopupMessageProvider achievementPopupMessageProvider)
    {
        _achievementsList = achievementsList;
        _achievementPopupMessageProvider = achievementPopupMessageProvider;
        SubscribeToAchievementsEvents();
        SubscribeToGameEvents();
    }

    private void SubscribeToAchievementsEvents()
    {
        foreach (Achievement achievement in _achievementsList.Achievements)
        {
            if (achievement.State != AchievementState.AchievementReceived)
            {
                achievement.OnAchievementReceived.AddListener(AchievementReceived);
            }
        }
    }

    private void SubscribeToGameEvents()
    {
        Game.OnLevelCreated.AddListener(CompareLevelAchievements);
        ScoreSystem.OnScoreUpdated.AddListener(CompareScoreAchievements);
        LevelCompleteChecker.OnDestructableObjectDestroyed.AddListener(CompareDestroyObjectInTimeAchievements);
    }

    private void CompareLevelAchievements(int level)
    {
        List<LevelAchievement> levelAchievements = _achievementsList.GetNotReceivedAchievementsByType<LevelAchievement>();
        foreach(LevelAchievement levelAchievement in levelAchievements)
        {
            levelAchievement.CompareLevel(level);
        }
    }

    private void CompareScoreAchievements(int score)
    {
        List<ScoreAchievement> scoreAchievements = _achievementsList.GetNotReceivedAchievementsByType<ScoreAchievement>();
        foreach (ScoreAchievement scoreAchievement in scoreAchievements)
        {
            scoreAchievement.CompareScore(score);
        }
    }
    private void CompareDestroyObjectInTimeAchievements()
    {
        List<DestroyObjectsInTimeAchievement> destroyObjectsInTimeAchievements = _achievementsList.GetNotReceivedAchievementsByType<DestroyObjectsInTimeAchievement>();
        foreach (DestroyObjectsInTimeAchievement destroyObjectsInTimeAchievement in destroyObjectsInTimeAchievements)
        {
            destroyObjectsInTimeAchievement.CompareDestroyObjectConditions();
        }
    }

    private void AchievementReceived(Achievement achievement)
    {
        _achievementPopupMessageProvider.TryShowPopupMessage(achievement);
        GameContext.Instance.SaveData.SerializableAchievementData[achievement.ID].AchievementState = achievement.State;
        GameContext.Instance.SaveData.SerializableBallData[achievement.Reward.Data.ID].IsUnlocked = true;
        GameContext.Instance.BinarySaveSystem.Save(GameContext.Instance.SaveData);
    }

    ~AchievementsHandler()
    {
        Game.OnLevelCreated.RemoveListener(CompareLevelAchievements);
        ScoreSystem.OnScoreUpdated.RemoveListener(CompareScoreAchievements);
        LevelCompleteChecker.OnDestructableObjectDestroyed.RemoveListener(CompareDestroyObjectInTimeAchievements);
    }
}