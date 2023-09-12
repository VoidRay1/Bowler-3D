using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class ScoreDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _scoreMultiplierText;
    [SerializeField] private LocalizedString _scoreLocalizedString;

    private void Awake()
    {
        _scoreLocalizedString.Add(Constants.LocalizationVariables.SCORE, new IntVariable());
    }

    private void OnEnable()
    {
        _scoreLocalizedString.StringChanged += ScoreChanged;
    }

    private void OnDisable()
    {
        _scoreLocalizedString.StringChanged -= ScoreChanged;
    }

    private void ScoreChanged(string score)
    {
        _scoreText.text = score;
    }

    public void DisplayScore(int score)
    {
        (_scoreLocalizedString[Constants.LocalizationVariables.SCORE] as IntVariable).Value = score;
    }

    public void DisplayScoreMultiplier(float scoreMultiplier)
    {
        _scoreMultiplierText.text = scoreMultiplier.ToString("0.0x");
    }
}