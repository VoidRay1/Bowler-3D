using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class AchievementsList : ScriptableObject
{
    [SerializeField] private List<Achievement> _achievements;

    public IReadOnlyList<Achievement> Achievements => _achievements;

    public List<T> GetAchievementsByType<T>() where T : Achievement
    {
        return _achievements.Where(achievement => achievement.GetType() == typeof(T)).Cast<T>().ToList();
    }

    public List<T> GetNotReceivedAchievementsByType<T>() where T : Achievement
    {
        return _achievements.Where(achievement => 
                                   achievement.State == AchievementState.AchievementNotReceived 
                                   && achievement.GetType() == typeof(T)).Cast<T>().ToList();
    }
}