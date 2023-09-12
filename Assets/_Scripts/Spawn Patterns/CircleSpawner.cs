using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CircleSpawner : ISpawnPattern
{
    private int _attemptsCount;
    private readonly UnityEvent<List<DestructableObject>> _onEntitiesSpawned = new UnityEvent<List<DestructableObject>>();
    private Transform _startSpawnPosition;

    public UnityEvent<List<DestructableObject>> OnEntitiesSpawned => _onEntitiesSpawned;

    public int AttemptsCount => _attemptsCount;

    public CircleSpawner(Transform startSpawnPosition)
    {
        _startSpawnPosition = startSpawnPosition;
    }

    public void Spawn(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                CircleSpawn();
                _attemptsCount = 3;
                break;
            case Difficulty.Medium:
               
                break;
            case Difficulty.Hard:
                
                break;
            case Difficulty.Impossible:
                
                break;
        }
    }

    private void CircleSpawn(int circlesCount = 3, float radius = 5.0f)
    {
        List<DestructableObject> destructableObjects = new List<DestructableObject>(circlesCount);
        DestructableObject randomDestructableObject = GameContext.Instance.DestructableObjectFactory.GetRandomDestructableObject();
        for (int i = 0; i < circlesCount; i++)
        {
            float angle = i * (2 * Mathf.PI / circlesCount);
            Vector3 spawnPosition = _startSpawnPosition.position + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * radius;
            DestructableObject destructableObject =
                Object.Instantiate(randomDestructableObject,
                spawnPosition, Quaternion.identity, _startSpawnPosition);

            destructableObjects.Add(destructableObject);
        }
        OnEntitiesSpawned?.Invoke(destructableObjects);
    }
}
