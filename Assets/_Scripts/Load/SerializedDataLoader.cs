using System.Collections.Generic;
using System.Linq;

public class SerializedDataLoader 
{
    public void LoadSerializedData()
    {
        GameContext.Instance.SaveData.SerializableAchievementData ??= new Dictionary<string, SerializableAchievementData>();
        GameContext.Instance.SaveData.SerializableBallData ??= new Dictionary<string, SerializableBallData>();
        InitializeAchievementsData();
        InitializeBallsData();
    }

    private void InitializeBallsData()
    {
        LoadBalls();
        Dictionary<string, SerializableBallData> ballsSerializableData = new Dictionary<string, SerializableBallData>(GameContext.Instance.BallsList.Balls.Count);
        foreach(BallUI ballUI in GameContext.Instance.BallsList.Balls)
        {
            SerializableBallData data = new SerializableBallData
            {
                ID = ballUI.Data.ID,
                IsUnlocked = ballUI.Data.IsUnlocked,
            };
            ballsSerializableData.Add(ballUI.Data.ID, data);
        }
        GameContext.Instance.SaveData.SerializableBallData = ballsSerializableData;
        GameContext.Instance.BinarySaveSystem.Save(GameContext.Instance.SaveData);
    }

    private void InitializeAchievementsData()
    {
        LoadAchivements();
        Dictionary<string, SerializableAchievementData> achievementsSerializableData = new Dictionary<string, SerializableAchievementData>(GameContext.Instance.AchievementsList.Achievements.Count);
        foreach (Achievement achievement in GameContext.Instance.AchievementsList.Achievements)
        {
            SerializableAchievementData data = new SerializableAchievementData
            {
                ID = achievement.ID,
                AchievementState = achievement.State,
            };
            achievementsSerializableData.Add(achievement.ID, data);
        }
        GameContext.Instance.SaveData.SerializableAchievementData = achievementsSerializableData;
        GameContext.Instance.BinarySaveSystem.Save(GameContext.Instance.SaveData);
    }

    private void LoadBalls()
    {
        foreach(var data in GameContext.Instance.SaveData.SerializableBallData)
        {
            BallUI ballUI = GameContext.Instance.BallsList.Balls.FirstOrDefault(ball => ball.Data.ID == data.Key);

            if (ballUI != null)
            {
                ballUI.Data.IsUnlocked = data.Value.IsUnlocked;
            }
        }
    }

    private void LoadAchivements()
    {
        foreach (var data in GameContext.Instance.SaveData.SerializableAchievementData)
        {
            Achievement achievement = GameContext.Instance.AchievementsList.Achievements.FirstOrDefault(achievement => achievement.ID == data.Key);

            if (achievement != null)
            {
                achievement.State = data.Value.AchievementState;
            }
        }
    }
}