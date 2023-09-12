using UnityEngine;

public class BallPreview : MonoBehaviour
{
    [SerializeField] private Transform _ballSelectionPreview;
    [SerializeField] private BallTemplate _ballTemplate;

    private BallTemplate _selectedBallTemplate;
    private BallUI _selectedBallUI;

    public void Display(string name, BallUI ballToPreview)
    {
        if (_selectedBallTemplate != null)
        {
            Destroy(_selectedBallTemplate.gameObject);
        }
        _selectedBallTemplate = Instantiate(_ballTemplate, _ballSelectionPreview);
        _selectedBallTemplate.Init(name, ballToPreview);
    }

    public void Display(BallUI ballToPreview)
    {
        if (_selectedBallUI != null)
        {
            Destroy(_selectedBallUI.gameObject);
        }
        _selectedBallUI = Instantiate(ballToPreview, _ballSelectionPreview);
    }

    public void Destroy()
    {
        if (_selectedBallTemplate != null)
        {
            Destroy(_selectedBallTemplate.gameObject);
        }
    }
}
