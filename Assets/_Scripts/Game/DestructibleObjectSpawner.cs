using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestructibleObjectSpawner : MonoBehaviour
{
    [SerializeField] private Transform _startSpawnParent;
    private ISpawnPattern _currentSpawnPattern;
    private List<ISpawnPattern> _spawnPatterns;
    private List<DestructableObject> _destructableObjects = new List<DestructableObject>();

    public ISpawnPattern SpawnPattern => _currentSpawnPattern;
    [HideInInspector] public UnityEvent<ISpawnPattern> OnSpawnPatternSwitched = new UnityEvent<ISpawnPattern>();

    private void Awake()
    {
        _spawnPatterns = new List<ISpawnPattern>()
        {
            new BowlingSpawner(_startSpawnParent),
            new SquareSpawner(_startSpawnParent),
            new CircleRandomSpawner(_startSpawnParent),
            new LineSpawner(_startSpawnParent)
        };
    }

    public void SwitchToRandomSpawner()
    {
        _currentSpawnPattern?.OnEntitiesSpawned.RemoveListener(SetEntities);
        _currentSpawnPattern = _spawnPatterns[Random.Range(0, _spawnPatterns.Count)];
        _currentSpawnPattern.OnEntitiesSpawned.AddListener(SetEntities);
        OnSpawnPatternSwitched?.Invoke(_currentSpawnPattern);
    }

    private void SetEntities(List<DestructableObject> destructableObjects)
    {
        _destructableObjects = destructableObjects;
    }

    public void Spawn(Difficulty difficulty)
    {
        DestroySpawnedObjects();
        _currentSpawnPattern.Spawn(difficulty);
    }

    private void DestroySpawnedObjects()
    {
        foreach(DestructableObject destructableObject in _destructableObjects)
        {
            Destroy(destructableObject.gameObject);
        }
        _destructableObjects.Clear();
    }
}