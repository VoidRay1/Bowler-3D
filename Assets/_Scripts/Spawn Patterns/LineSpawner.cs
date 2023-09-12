using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LineSpawner : ISpawnPattern
{
    private int _attemptsCount;
    private readonly UnityEvent<List<DestructableObject>> _onEntitiesSpawned = new UnityEvent<List<DestructableObject>>();
    private Transform _startSpawnTransform;

    public int AttemptsCount => _attemptsCount;
    public UnityEvent<List<DestructableObject>> OnEntitiesSpawned => _onEntitiesSpawned;

    public LineSpawner(Transform startSpawnTransform)
    {
        _startSpawnTransform = startSpawnTransform;
    }

    public void Spawn(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                LineSpawn(3);
                _attemptsCount = 4;
                break;
            case Difficulty.Medium:
                LineSpawn(4);
                _attemptsCount = 4;
                break;
            case Difficulty.Hard:
                LineSpawn(5);
                _attemptsCount = 4;
                break;
            case Difficulty.Impossible:
                LineSpawn(6);
                _attemptsCount = 4;
                break;
        }
    }

    private void LineSpawn(int countInLine)
    {
        List<DestructableObject> destructableObjects = new List<DestructableObject>(countInLine);
        DestructableObject randomDestructableObject = GameContext.Instance.DestructableObjectFactory.GetRandomDestructableObject();
        float step = randomDestructableObject.transform.localScale.x / 10;
        float xOffsetPosition = countInLine % 2 == 0
            ? -step * countInLine + step
            : -step * (countInLine - 1);
        for (int i = 0; i < countInLine; i++)
        {
            DestructableObject destructableObject =
                            Object.Instantiate(randomDestructableObject,
                            _startSpawnTransform.position + new Vector3(xOffsetPosition, 0, 0), Quaternion.identity, _startSpawnTransform);

            destructableObjects.Add(destructableObject);
            xOffsetPosition += step * 2;
        }
        OnEntitiesSpawned?.Invoke(destructableObjects);
    }
}