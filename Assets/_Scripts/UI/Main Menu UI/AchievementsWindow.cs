using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(SwipeAnimation))]
public class AchievementsWindow : MonoBehaviour, IOverlayWindow
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RootRaycastHandler _rootRaycastHandler;
    [SerializeField] private AchievementTemplate _achievementTemplate;
    [SerializeField] private Transform _achievementsParent;
    [SerializeField] private Button _closeButton;
    private SwipeAnimation _swipeAnimation;

    private readonly List<AchievementTemplate> _achievementTemplates = new List<AchievementTemplate>();
    private readonly UnityEvent _onOverlayWindowOpened = new UnityEvent();
    private readonly UnityEvent _onOverlayWindowClosed = new UnityEvent();

    public UnityEvent OnOverlayWindowOpened => _onOverlayWindowOpened;
    public UnityEvent OnOverlayWindowClosed => _onOverlayWindowClosed;

    private void Start()
    {
        _rootRaycastHandler.RegisterOverlayWindowListener(this);
        _swipeAnimation = GetComponent<SwipeAnimation>();
        _swipeAnimation.OnReturnAnimationEnded.AddListener(Disable);
        _swipeAnimation.OnMoveToCanvasCenterEnded.AddListener(EnableCloseButton);
        Disable();
    }

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        _rootRaycastHandler.UnRegisterOverlayWindowListener(this);
        _swipeAnimation.OnReturnAnimationEnded.RemoveListener(Disable);
        _swipeAnimation.OnMoveToCanvasCenterEnded.RemoveListener(EnableCloseButton);
        _closeButton.onClick.RemoveListener(Hide);
    }

    public void ShowAchievements(IReadOnlyList<Achievement> achievements)
    {
        Clear();
        foreach(Achievement achievement in achievements)
        {
            AchievementTemplate achievementTemplate = Instantiate(_achievementTemplate, _achievementsParent);
            achievementTemplate.Init(achievement.Title, achievement.Description, achievement.State);
            _achievementTemplates.Add(achievementTemplate);
        }

        _closeButton.gameObject.SetActive(false);
        OnOverlayWindowOpened?.Invoke();
        _canvas.enabled = true;
        StartCoroutine(_swipeAnimation.MoveToCanvasCenter(new Vector2(0.0f, 0.0f)));
    }

    private void Hide()
    {
        StartCoroutine(_swipeAnimation.ReturnToOriginPosition());
    }

    private void EnableCloseButton()
    {
        _closeButton.gameObject.SetActive(true);
    }

    private void Disable()
    {
        OnOverlayWindowClosed?.Invoke();
        _canvas.enabled = false;
        Clear();
    }

    private void Clear()
    {
        foreach (AchievementTemplate achievementTemplate in _achievementTemplates)
        {
            Destroy(achievementTemplate.gameObject);
        }
        _achievementTemplates.Clear();
    }
}