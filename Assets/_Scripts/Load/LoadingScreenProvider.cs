using System.Collections.Generic;

public class LoadingScreenProvider : AssetLoader
{
    public async void Load(Queue<ILoadingOperation> loadingOperations)
    {
        LoadingScreen loadingScreen = await Load<LoadingScreen>(Constants.AddressablesIds.LOADING_SCREEN);
        await loadingScreen.Load(loadingOperations);
        Unload(loadingScreen.gameObject);
    }
}