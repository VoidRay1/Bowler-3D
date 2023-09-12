using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LocalizationLoadingOperation : ILoadingOperation
{
    private readonly SaveData _saveData;
    public string Description => "";

    public LocalizationLoadingOperation(SaveData saveData)
    {
        _saveData = saveData;
    }

    public async Task Load(Action<float> onProgress)
    {
        onProgress?.Invoke(0.25f);

        AsyncOperationHandle<LocalizationSettings> localizationAsyncOperation = LocalizationSettings.InitializationOperation;
        await localizationAsyncOperation.Task;

        onProgress?.Invoke(0.75f);

        if (_saveData.IsGameStartedFirstTime)
        {
            SelectSystemLanguage();
        }
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_saveData.LocaleIndex];

        onProgress?.Invoke(1.0f);
    }

    private void SelectSystemLanguage()
    {
        switch (Application.systemLanguage)
        {
            case SystemLanguage.English:
                _saveData.LocaleIndex = 0;
                break;
            case SystemLanguage.French:
                _saveData.LocaleIndex = 1;
                break;
            case SystemLanguage.German:
                _saveData.LocaleIndex = 2;
                break;
            case SystemLanguage.Russian:
                _saveData.LocaleIndex = 3;
                break;
            case SystemLanguage.Ukrainian:
                _saveData.LocaleIndex = 4;
                break;
            default:
                _saveData.LocaleIndex = 0;
                break;
        }
    }
}