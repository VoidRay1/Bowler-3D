using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class LevelDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private LocalizedString _localizedLevelText;

    private void Awake()
    {
        _localizedLevelText.Add(Constants.LocalizationVariables.LEVEL, new IntVariable());
    }

    private void OnEnable()
    {
        _localizedLevelText.StringChanged += LevelChanged;
    }

    private void OnDisable()
    {
        _localizedLevelText.StringChanged -= LevelChanged;
    }

    private void LevelChanged(string level)
    {
        _levelText.text = level;
    }

    public void DisplayLevel(int level)
    {
        (_localizedLevelText[Constants.LocalizationVariables.LEVEL] as IntVariable).Value = level;
    }
}