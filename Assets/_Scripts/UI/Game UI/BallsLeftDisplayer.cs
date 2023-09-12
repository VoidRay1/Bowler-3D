using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class BallsLeftDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _ballsLeftText;
    [SerializeField] private LocalizedString _localizedBallsLeft;

    private void Awake()
    {
        _localizedBallsLeft.Add(Constants.LocalizationVariables.BALLS_LEFT, new IntVariable());
    }

    private void OnEnable()
    {
        _localizedBallsLeft.StringChanged += BallsLeftTextChanged;
    }

    private void OnDisable()
    {
        _localizedBallsLeft.StringChanged -= BallsLeftTextChanged;
    }

    private void BallsLeftTextChanged(string value)
    {
        _ballsLeftText.text = value;
    }

    public void DisplayBallsLeft(int ballsLeft)
    {
        (_localizedBallsLeft[Constants.LocalizationVariables.BALLS_LEFT] as IntVariable).Value = ballsLeft;
    }
}