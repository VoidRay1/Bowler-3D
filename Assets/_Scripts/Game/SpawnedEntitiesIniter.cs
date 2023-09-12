using System.Collections.Generic;
using UnityEngine;

public class SpawnedEntitiesIniter : MonoBehaviour
{
    [SerializeField] private DestructibleObjectSpawner _destructibleObjectSpawner;
    [SerializeField] private ScoreMultiplierHandler _scoreMultiplierHandler;
    [SerializeField] private LevelCompleteChecker _levelCompleteChecker;

    private List<DestructableObject> _destructableObjects;

    private void OnEnable()
    {
        _destructibleObjectSpawner.OnSpawnPatternSwitched.AddListener(SubscribeOnSpawnEntities);
    }

    private void OnDisable()
    {
        _destructibleObjectSpawner.OnSpawnPatternSwitched.RemoveListener(SubscribeOnSpawnEntities);
    }

    private void SubscribeOnSpawnEntities(ISpawnPattern spawnPattern)
    {
        spawnPattern.OnEntitiesSpawned.AddListener(SetEntities);
    }

    private void SetEntities(List<DestructableObject> destructableObjects)
    {
        _destructableObjects = destructableObjects;
        _scoreMultiplierHandler.Init(_destructableObjects);
        _levelCompleteChecker.Init(_destructableObjects);
    }
}