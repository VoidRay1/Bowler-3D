using UnityEngine;

public class Ball : MonoBehaviour, IPhysicsInteractable
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private BallBehaviour _ballBehaviour;
    [SerializeField] private MaterialType _materialType;
    [SerializeField] private float _ballStrength = 20.0f;
    private readonly InteractableHandler _interactableHandler = new InteractableHandler();

    public MaterialType MaterialType => _materialType;
    public Rigidbody Rigidbody => _rigidbody;
    public float Strength => _ballStrength;

    public void Init(RespawnBallUI respawnBallUI)
    {
        _ballBehaviour.Init(respawnBallUI, _rigidbody);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out DestructableObject destructableObject))
        {
            destructableObject.TryDealDamage(_ballStrength * _rigidbody.velocity.magnitude);
        }
        if(collision.collider.TryGetComponent(out IInteractable interactable))
        {
            _interactableHandler.InteractablesEnter(this, interactable);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IInteractable interactable))
        {
            _interactableHandler.InteractablesStay(this, interactable);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IInteractable interactable))
        {
            _interactableHandler.InteractablesExit(this, interactable);
        }
    }
}