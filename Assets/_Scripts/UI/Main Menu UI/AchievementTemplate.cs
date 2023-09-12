using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementTemplate : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Image _achievementImage;
    [SerializeField] private Image _achievementUnlockedImage;
    
    public void Init(string title, string description, AchievementState achievementState)
    {
        _titleText.text = title;
        _descriptionText.text = description;
        _achievementUnlockedImage.enabled = achievementState == AchievementState.AchievementReceived;
    }
}