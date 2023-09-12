using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.SceneManagement;

public class MenuLoadingOperation : ILoadingOperation
{
    private readonly LocalizedString _menuLoadLocalizedDescription = new LocalizedString() 
    { 
        TableReference = Constants.MAIN_LOCALIZATION_TABLE,
        TableEntryReference = Constants.LocalizationVariables.MAIN_MENU_LOADING
    };

    public string Description => _menuLoadLocalizedDescription.GetLocalizedString();

    public async Task Load(Action<float> onProgress)
    {
        onProgress?.Invoke(0.25f);
        AsyncOperation menuLoadOperation = SceneManager.LoadSceneAsync(Constants.Scenes.MAIN_MENU_SCENE, LoadSceneMode.Additive);
        while(menuLoadOperation.isDone == false)
        {
           await Task.Delay(1);
        }
        onProgress?.Invoke(1.0f);
    }
}