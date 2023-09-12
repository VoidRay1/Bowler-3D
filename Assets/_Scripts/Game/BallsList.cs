using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BallsList : ScriptableObject
{
    [SerializeField] private List<BallUI> _balls;

    public IReadOnlyList<BallUI> Balls => _balls;

    public BallUI FindBallByID(string id)
    {
        return _balls.Find(ball => ball.Data.ID == id);
    }
}