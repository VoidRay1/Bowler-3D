using UnityEngine;

public class PopupPositionCalculator : MonoBehaviour
{
    public RectTransform CalculateStartPosition(RectTransform rectTransform, AnchorPreset anchor, Vector3 position)
    {
        rectTransform.SetAnchor(anchor);
        rectTransform.anchoredPosition = position;
        return rectTransform;
    }
}
