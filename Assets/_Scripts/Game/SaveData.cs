using System.Collections.Generic;

[System.Serializable]
public class SaveData 
{
    public int MaxScore;
    public byte LocaleIndex;
    public float Volume;
    public bool IsGameStartedFirstTime = true;

    public string SelectedBallID;

    public Dictionary<string, SerializableBallData> SerializableBallData;
    public Dictionary<string, SerializableAchievementData> SerializableAchievementData;
}