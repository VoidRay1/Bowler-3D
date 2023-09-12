using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    [SerializeField] private Vector3 _centerOfMass;
    [SerializeField] private float _sphereRadius = 0.2f;
    [SerializeField] private Rigidbody _rigidbody;

    private void OnValidate()
    {
        _rigidbody.centerOfMass = _centerOfMass;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_rigidbody.worldCenterOfMass, _sphereRadius);
    }
}
