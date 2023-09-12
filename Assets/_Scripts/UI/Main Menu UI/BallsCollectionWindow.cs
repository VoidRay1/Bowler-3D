using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(SwipeAnimation))]
public class BallsCollectionWindow : MonoBehaviour, IOverlayWindow
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private BallPreview _collectionBallPreview;
    [SerializeField] private BallPreview _swipeAreaBallPreview;
    [SerializeField] private RootRaycastHandler _rootRaycastHandler;
    [SerializeField] private BallTemplate _ballTemplate;
    [SerializeField] private Transform _ballSelectionPreview;
    [SerializeField] private Transform _collectionParent;
    [SerializeField] private Button _closeButton;

    private SwipeAnimation _swipeAnimation;
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

    public void ShowCollection(IReadOnlyList<BallUI> balls, BallUI ballToPreview)
    {
        Clear();
        _collectionBallPreview.Display(ballToPreview.Data.Name, ballToPreview);

        foreach (BallUI ball in balls)
        {
            BallTemplate ballTemplate = Instantiate(_ballTemplate, _collectionParent);
            ballTemplate.Init(ball.Data.Name, ball, BallSelected);
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
        _collectionBallPreview.Destroy();
        foreach (Transform child in _collectionParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void BallSelected(BallUI ballUI)
    {
        if (ballUI.Data.IsUnlocked)
        {
            _collectionBallPreview.Display(ballUI.Data.Name, ballUI);
            GameContext.Instance.SelectedBall = GameContext.Instance.BallsList.FindBallByID(ballUI.Data.ID);
            GameContext.Instance.SaveData.SelectedBallID = ballUI.Data.ID;
            GameContext.Instance.BinarySaveSystem.Save(GameContext.Instance.SaveData);
            _swipeAreaBallPreview.Display(ballUI);
        }
    }
}