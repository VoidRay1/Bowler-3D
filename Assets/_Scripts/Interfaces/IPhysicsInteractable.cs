using UnityEngine;

public interface IPhysicsInteractable : IInteractable
{
    Rigidbody Rigidbody { get; }
    float Strength { get; }
}