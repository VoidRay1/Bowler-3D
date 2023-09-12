using System.Collections.Generic;
using UnityEngine.Events;

public interface ISpawnPattern 
{
    int AttemptsCount { get; }
    UnityEvent<List<DestructableObject>> OnEntitiesSpawned { get; }
    void Spawn(Difficulty levelDifficulty);
}