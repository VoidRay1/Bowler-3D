using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CircleRandomSpawner : ISpawnPattern
{
    private int _attemptsCount;
    private readonly UnityEvent<List<DestructableObject>> _onEntitiesSpawned = new UnityEvent<List<DestructableObject>>();
    private Transform _startSpawnTransform;

    public int AttemptsCount => _attemptsCount;
    public UnityEvent<List<DestructableObject>> OnEntitiesSpawned => _onEntitiesSpawned;

    public CircleRandomSpawner(Transform startSpawnTransform)
    {
        _startSpawnTransform = startSpawnTransform;
    }

    public void Spawn(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                RandomInCircleSpawn(2, 1.8f);
                _attemptsCount = 4;
                break;
            case Difficulty.Medium:
                RandomInCircleSpawn(4, 2.7f);
                _attemptsCount = 5;
                break;
            case Difficulty.Hard:
                RandomInCircleSpawn(6, 3.5f);
                _attemptsCount = 7;
                break;
            case Difficulty.Impossible:
                int randomObjectsCount = Random.Range(10, 15);
                RandomInCircleSpawn(randomObjectsCount, 4.0f);
                _attemptsCount = randomObjectsCount;
                break;
        }
    }

    private void RandomInCircleSpawn(int objectsCount, float radius)
    {
        List<DestructableObject> destructableObjects = new List<DestructableObject>();
        float sphereRadius = 0.6f;
        DestructableObject randomDestructableObject = GameContext.Instance.DestructableObjectFactory.GetRandomDestructableObject();
        Vector2 startSpawnTransform = new Vector2(_startSpawnTransform.position.x, _startSpawnTransform.position.z);
        for (int i = 0; i < objectsCount; i++)
        {
            Vector2 randomPosition = startSpawnTransform + (Random.insideUnitCircle * radius);
            while(Physics.OverlapSphere(new Vector3(randomPosition.x, 0, randomPosition.y), sphereRadius).Length > 0)
            {
                randomPosition = startSpawnTransform + (Random.insideUnitCircle * radius);
            }
            DestructableObject destructableObject =
                Object.Instantiate(randomDestructableObject,
                new Vector3(randomPosition.x, 0, randomPosition.y), Quaternion.identity, _startSpawnTransform);

            destructableObjects.Add(destructableObject);
        }
        OnEntitiesSpawned?.Invoke(destructableObjects);
    }
}