using UnityEngine;

public static class RectTransformExtensions 
{
    public static void SetAnchor(this RectTransform rectTransform, AnchorPreset anchorPreset)
    {
        switch(anchorPreset)
        {
            case AnchorPreset.TopLeft:
                rectTransform.anchorMin = new Vector2(0.0f, 1.0f);
                rectTransform.anchorMax = new Vector2(0.0f, 1.0f);
                break;
            case AnchorPreset.TopCenter:
                rectTransform.anchorMin = new Vector2(0.5f, 1.0f);
                rectTransform.anchorMax = new Vector2(0.5f, 1.0f);
                break;
            case AnchorPreset.TopRight:
                rectTransform.anchorMin = new Vector2(1.0f, 1.0f);
                rectTransform.anchorMax = new Vector2(1.0f, 1.0f);
                break;
            case AnchorPreset.MiddleLeft:
                rectTransform.anchorMin = new Vector2(0.0f, 0.5f);
                rectTransform.anchorMax = new Vector2(0.0f, 0.5f);
                break;
            case AnchorPreset.MiddleCenter:
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                break;
            case AnchorPreset.MiddleRight:
                rectTransform.anchorMin = new Vector2(1.0f, 0.5f);
                rectTransform.anchorMax = new Vector2(1.0f, 0.5f);
                break;
            case AnchorPreset.BottomLeft:
                rectTransform.anchorMin = new Vector2(0.0f, 0.0f);
                rectTransform.anchorMax = new Vector2(0.0f, 0.0f);
                break;
            case AnchorPreset.BottonCenter:
                rectTransform.anchorMin = new Vector2(0.5f, 0.0f);
                rectTransform.anchorMax = new Vector2(0.5f, 0.0f);
                break;
            case AnchorPreset.BottomRight:
                rectTransform.anchorMin = new Vector2(1.0f, 0.0f);
                rectTransform.anchorMax = new Vector2(1.0f, 0.0f);
                break;
        }
    }
}