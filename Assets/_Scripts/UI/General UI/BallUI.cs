using UnityEngine;

public class BallUI : MonoBehaviour
{
    [SerializeField] private BallData _data;

    public BallData Data => _data;
}