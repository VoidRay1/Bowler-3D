using System.Collections.Generic;
using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    private void Start()
    {
        GameContext.Instance.Initialize();

        SerializedDataLoader serializedDataLoader = new SerializedDataLoader();
        serializedDataLoader.LoadSerializedData();

        Queue<ILoadingOperation> operations = new Queue<ILoadingOperation>();
        operations.Enqueue(new LocalizationLoadingOperation(GameContext.Instance.SaveData));
        operations.Enqueue(new DataInitializeLoadingOperation(GameContext.Instance.SaveData));
        operations.Enqueue(new MenuLoadingOperation());

        GameContext.Instance.LoadingScreenProvider.Load(operations);
    }
}