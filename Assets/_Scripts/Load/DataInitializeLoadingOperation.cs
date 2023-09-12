using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;

public class DataInitializeLoadingOperation : ILoadingOperation
{
    private readonly LocalizedString _menuLoadLocalizedDescription = new LocalizedString() 
    { 
        TableReference = Constants.MAIN_LOCALIZATION_TABLE, 
        TableEntryReference = Constants.LocalizationVariables.DATA_LOADING 
    };
    private readonly SaveData _saveData;

    public string Description => _menuLoadLocalizedDescription.GetLocalizedString();

    public DataInitializeLoadingOperation(SaveData saveData)
    {
        _saveData = saveData;
    }

    public async Task Load(Action<float> onProgress)
    {
        onProgress?.Invoke(0.25f);

        if (_saveData.IsGameStartedFirstTime)
        {
            _saveData.Volume = 0.8f;
            _saveData.IsGameStartedFirstTime = false;
            GameContext.Instance.BinarySaveSystem.Save(_saveData);
        }

        onProgress?.Invoke(0.75f);

        Application.targetFrameRate = Constants.STANDART_REFRESH_RATE;
        AudioListener.volume = _saveData.Volume;
        BallUI ballUI = GameContext.Instance.BallsList.FindBallByID(_saveData.SelectedBallID);
        if (ballUI != null)
        {
            GameContext.Instance.SelectedBall = ballUI;
        }
        else
        {
            GameContext.Instance.SelectedBall = GameContext.Instance.FalbackBall;
        }

        onProgress?.Invoke(1.0f);
    }
}