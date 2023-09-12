using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class DestructableObject : MonoBehaviour, IPhysicsInteractable
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private MaterialType _materialType;
    [SerializeField] private float _destructibleObjectStrength = 5.0f;
    [SerializeField] private int _destructibleReward;
    [SerializeField] private float _strengthToDestroy;
    private readonly InteractableHandler _interactableHandler = new InteractableHandler();

    public float StrengthToDestroy => _strengthToDestroy;
    public MaterialType MaterialType => _materialType;
    public int DestructionReward => _destructibleReward;
    public Rigidbody Rigidbody => _rigidbody;
    public float Strength => _destructibleObjectStrength;

    [HideInInspector] public UnityEvent<DestructableObject> OnDestructableObjectDestroyed = new UnityEvent<DestructableObject>();

    public void TryDealDamage(float damage)
    {
        if(_strengthToDestroy < damage)
        {
            OnDestructableObjectDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out IInteractable interactable))
        {
            _interactableHandler.InteractablesEnter(this, interactable);
        }
        if (collision.collider.TryGetComponent(out DestructableObject _))
        {
            TryDealDamage(_rigidbody.velocity.magnitude * _destructibleObjectStrength);
        }
    }
}