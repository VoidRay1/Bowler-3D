using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SwipeAnimation))]
public class AchievementPopupMessage : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private PopupPositionCalculator _popupPositionCalculator;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private Image _achievementImage;
    [SerializeField] private Image _rewardImage;
    [SerializeField, Range(1, 100)] private int _percentsOffsetFromCamera;

    private SwipeAnimation _swipeAnimation;
    private Action _onAchievementPopupMessageHide;

    private void Start()
    {
        _swipeAnimation = GetComponent<SwipeAnimation>();
        _swipeAnimation.OnReturnAnimationEnded.AddListener(Disable);
        _swipeAnimation.OnMoveToCanvasCenterEnded.AddListener(Hide);
        _canvas.enabled = false;
    }

    private void OnDisable()
    {
        _swipeAnimation.OnReturnAnimationEnded.RemoveListener(Disable);
        _swipeAnimation.OnMoveToCanvasCenterEnded.RemoveListener(Hide);
    }

    public void Init(AnchorPreset anchor, Vector3 position, string description, Action onAchievementPopupMessageHide)
    {
        _rectTransform = _popupPositionCalculator.CalculateStartPosition(_rectTransform, anchor, position);
        _descriptionText.text = description;
        _onAchievementPopupMessageHide = onAchievementPopupMessageHide;
    }

    public void Show()
    {
        _canvas.enabled = true;
        StartCoroutine(_swipeAnimation.MoveToCanvasCenter(new Vector2(0.0f, Screen.height / 2 * _percentsOffsetFromCamera / 100)));
    }

    private void Hide()
    {
        StartCoroutine(_swipeAnimation.ReturnToOriginPosition());
    }

    private void Disable()
    {
        _canvas.enabled = false;
        _onAchievementPopupMessageHide?.Invoke();
    }

    public Vector2 GetSizeDelta()
    {
        return _rectTransform.sizeDelta;
    }
}
