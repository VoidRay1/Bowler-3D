using UnityEngine;

public class Environment : MonoBehaviour, IInteractable
{
    [SerializeField] private MaterialType _materialType;

    public MaterialType MaterialType => _materialType;
}