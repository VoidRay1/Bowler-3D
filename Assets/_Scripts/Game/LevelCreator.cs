using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private DestructibleObjectSpawner _destructibleObjectSpawner;

    public void CreateNextLevel(Difficulty difficulty)
    {
        _destructibleObjectSpawner.SwitchToRandomSpawner();
        _destructibleObjectSpawner.Spawn(difficulty);
    }
}