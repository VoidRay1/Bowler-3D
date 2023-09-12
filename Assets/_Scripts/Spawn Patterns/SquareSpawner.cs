using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SquareSpawner : ISpawnPattern
{
    private int _attemptsCount;
    private readonly UnityEvent<List<DestructableObject>> _onEntitiesSpawned = new UnityEvent<List<DestructableObject>>();
    private Transform _startSpawnTransform;

    public UnityEvent<List<DestructableObject>> OnEntitiesSpawned => _onEntitiesSpawned;

    public int AttemptsCount => _attemptsCount;

    public SquareSpawner(Transform startSpawnTransform)
    {
        _startSpawnTransform = startSpawnTransform;
    }

    public void Spawn(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                SquareSpawn(2, 2);
                _attemptsCount = 4;
                break;
            case Difficulty.Medium:
                SquareSpawn(3, 2);
                _attemptsCount = 4;
                break;
            case Difficulty.Hard:
                SquareSpawn(3, 3);
                _attemptsCount = 5;
                break;
            case Difficulty.Impossible:
                SquareSpawn(2, 4);
                _attemptsCount = 4;
                break;
        }
    }

    private void SquareSpawn(int rows, int columns)
    {
        List<DestructableObject> destructableObjects = new List<DestructableObject>();
        float xOffsetPosition = columns % 2 == 0
            ? -(columns / 2 - 0.5f)
            : -(columns / 2);
        float tempXOffsetPosition;
        DestructableObject randomDestructableObject = GameContext.Instance.DestructableObjectFactory.GetRandomDestructableObject();
        for (int i = 0; i < rows; i++)
        {
            tempXOffsetPosition = xOffsetPosition;
            for (int j = 0; j < columns; j++)
            {
                DestructableObject destructableObject =
                                Object.Instantiate(randomDestructableObject,
                                _startSpawnTransform.position + new Vector3(tempXOffsetPosition, 0, i), Quaternion.identity, _startSpawnTransform);

                destructableObjects.Add(destructableObject);
                tempXOffsetPosition += 1;
            }
        }
        OnEntitiesSpawned?.Invoke(destructableObjects);
    }
}