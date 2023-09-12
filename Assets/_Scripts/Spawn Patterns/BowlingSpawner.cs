using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BowlingSpawner : ISpawnPattern
{
    private int _attemptsCount;
    private readonly UnityEvent<List<DestructableObject>> _onEntitiesSpawned = new UnityEvent<List<DestructableObject>>();
    private Transform _startSpawnTransform;

    public UnityEvent<List<DestructableObject>> OnEntitiesSpawned => _onEntitiesSpawned;

    public int AttemptsCount => _attemptsCount;

    public BowlingSpawner(Transform startSpawnTransform)
    {
        _startSpawnTransform = startSpawnTransform;
    }

    public void Spawn(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                BowlingSpawn(2);
                _attemptsCount = 3;
                break;
            case Difficulty.Medium: 
                BowlingSpawn(3);
                _attemptsCount = 3;
                break;
            case Difficulty.Hard: 
                BowlingSpawn(4);
                _attemptsCount = 5;
                break;
            case Difficulty.Impossible: 
                BowlingSpawn(4);
                _attemptsCount = 4;
                break;
        }
    }

    private void BowlingSpawn(int rows = 4)
    {
        List<DestructableObject> destructableObjects = new List<DestructableObject>();
        DestructableObject randomDestructableObject = GameContext.Instance.DestructableObjectFactory.GetRandomDestructableObject();
        float step = randomDestructableObject.transform.localScale.x / 10;
        float xOffset;
        for (int i = 0; i < rows; i++)
        {
            xOffset = -step * i;
            for (int j = 0; j < i + 1; j++)
            {
                DestructableObject destructableObject =
                    Object.Instantiate(randomDestructableObject,
                    _startSpawnTransform.position + new Vector3(xOffset, 0, i), Quaternion.identity, _startSpawnTransform);

                destructableObjects.Add(destructableObject);
                xOffset += step * 2;
            }
        }
        OnEntitiesSpawned?.Invoke(destructableObjects);
    }
}