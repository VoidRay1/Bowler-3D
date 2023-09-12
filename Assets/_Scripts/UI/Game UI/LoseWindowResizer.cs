using UnityEngine;

public class LoseWindowResizer : MonoBehaviour
{
    [SerializeField] private RectTransform _panelTransform;
    [SerializeField] private RectTransform _currentScoreTransform;
    [SerializeField] private RectTransform _maxScoreTransform;

    public void ResizeLoseWindow(Vector2 panelSizeDelta, Vector2 currentScorePosition, Vector2 maxScorePosition)
    {
        _panelTransform.sizeDelta = new Vector2(panelSizeDelta.x, panelSizeDelta.y);
        _currentScoreTransform.anchoredPosition = new Vector2(currentScorePosition.x, currentScorePosition.y);
        _maxScoreTransform.anchoredPosition = new Vector2(maxScorePosition.x, maxScorePosition.y);
    }
}