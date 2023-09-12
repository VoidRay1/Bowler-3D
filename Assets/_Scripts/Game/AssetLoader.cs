using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetLoader 
{
    private GameObject _cachedGameObject;

    public async Task<T> Load<T>(string assetId, Transform parent = null)
    {
        AsyncOperationHandle<GameObject> addresablesAsyncOperation = Addressables.InstantiateAsync(assetId, parent);
        _cachedGameObject = await addresablesAsyncOperation.Task;
        if(_cachedGameObject.TryGetComponent(out T component) == false)
        {
            throw new NullReferenceException($"{typeof(T)} is null");
        }
        return component;
    }

    public void Unload(GameObject gameObject) 
    {
        if(gameObject == null)
        {
            return;
        }
        gameObject.SetActive(false);
        Addressables.ReleaseInstance(gameObject);
    }

    public void UnloadCachedGameObject()
    {
        if (_cachedGameObject == null)
        {
            return;
        }
        _cachedGameObject.SetActive(false);
        Addressables.ReleaseInstance(_cachedGameObject);
        _cachedGameObject = null;
    }
}