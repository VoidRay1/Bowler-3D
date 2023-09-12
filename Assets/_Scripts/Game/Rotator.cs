using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Vector3 _eulers;

    private void Update()
    {
        transform.Rotate(_eulers * _rotateSpeed);
    }
}