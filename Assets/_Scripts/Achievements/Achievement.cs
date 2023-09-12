using System;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class Achievement : ScriptableObject
{
    [SerializeField, HideInInspector] private string _id;
    [SerializeField] private string _title;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _achievementSprite;
    [SerializeField] private BallUI _reward;

    [NonSerialized] public AchievementState State = AchievementState.AchievementNotReceived;

    [HideInInspector] public UnityEvent<Achievement> OnAchievementReceived = new UnityEvent<Achievement>();
    public string ID => _id;
    public string Title => _title;
    public string Description => _description;
    public Sprite AchievementSprite => _achievementSprite;
    public BallUI Reward => _reward;

#if UNITY_EDITOR
    protected void TryGenerateGUID()
    {
        if (string.IsNullOrEmpty(ID))
        {
            _id = Guid.NewGuid().ToString();
            EditorUtility.SetDirty(this);
        }
    }

    [ContextMenu("Reset id")]
    private void ResetId()
    {
        _id = Guid.NewGuid().ToString();
    }
#endif
}

[Serializable]
public class SerializableAchievementData
{
    public string ID;
    public AchievementState AchievementState;
}