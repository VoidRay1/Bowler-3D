using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UI;

public class LoseWindow : MonoBehaviour, IOverlayWindow
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private LoseWindowResizer _loseWindowResizer;
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private TMP_Text _maxScoreText;
    [SerializeField] private Button _adButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private LocalizedString _currentScoreLocalizedString;
    [SerializeField] private LocalizedString _maxScoreLocalizedString;

    [SerializeField] private Vector2 _panelSizeDelta;
    [SerializeField] private Vector2 _currentScorePosition;
    [SerializeField] private Vector2 _maxScorePosition;

    private RootRaycastHandler _rootRaycastHandler;
    private Action _onMenuClicked;
    private Action _onRestartClicked;
    private Action _onAdClicked;
    private readonly UnityEvent _onOverlayWindowOpened = new UnityEvent();
    private readonly UnityEvent _onOverlayWindowClosed = new UnityEvent();

    public UnityEvent OnOverlayWindowOpened => _onOverlayWindowOpened;
    public UnityEvent OnOverlayWindowClosed => _onOverlayWindowClosed;

    private void Awake()
    {
        _currentScoreLocalizedString.Add(Constants.LocalizationVariables.CURRENT_SCORE, new IntVariable());
        _maxScoreLocalizedString.Add(Constants.LocalizationVariables.MAX_SCORE, new IntVariable());
    }

    private void Start()
    {
        _canvas.enabled = false;
    }

    private void OnEnable()
    {
        _adButton.onClick.AddListener(ShowRewardAd);
        _restartButton.onClick.AddListener(RestartButtonClicked);
        _mainMenuButton.onClick.AddListener(MainMenuButtonClicked);
        _currentScoreLocalizedString.StringChanged += CurrentScoreChanged;
        _maxScoreLocalizedString.StringChanged += MaxScoreChanged;
    }

    private void OnDisable()
    {
        _adButton.onClick.RemoveListener(ShowRewardAd);
        _restartButton.onClick.RemoveListener(RestartButtonClicked);
        _mainMenuButton.onClick.RemoveListener(MainMenuButtonClicked);
        _currentScoreLocalizedString.StringChanged -= CurrentScoreChanged;
        _maxScoreLocalizedString.StringChanged -= MaxScoreChanged;
        _rootRaycastHandler.UnRegisterOverlayWindowListener(this);
    }

    public void SetRootRaycastHandler(RootRaycastHandler rootRaycastHandler)
    {
        _rootRaycastHandler = rootRaycastHandler;
        _rootRaycastHandler.RegisterOverlayWindowListener(this);
    }

    public void ShowWindow(int currentScore, int maxScore, Action onMenuClicked, Action onRestartClicked, Action onAdClicked, bool showAdButton)
    {
        OnOverlayWindowOpened?.Invoke();
        _canvas.enabled = true;

        (_currentScoreLocalizedString[Constants.LocalizationVariables.CURRENT_SCORE] as IntVariable).Value = currentScore;
        (_maxScoreLocalizedString[Constants.LocalizationVariables.MAX_SCORE] as IntVariable).Value = maxScore;
        _onMenuClicked = onMenuClicked;
        _onRestartClicked = onRestartClicked;
        _onAdClicked = onAdClicked;

        _adButton.gameObject.SetActive(showAdButton);
        if (showAdButton == false)
        {
            _loseWindowResizer.ResizeLoseWindow(_panelSizeDelta, _currentScorePosition, _maxScorePosition);
        }
    }

    private void CurrentScoreChanged(string score)
    {
        _currentScoreText.text = score;
    }

    private void MaxScoreChanged(string maxScore)
    {
        _maxScoreText.text = maxScore;
    }

    private void ShowRewardAd()
    {
        OnOverlayWindowClosed?.Invoke();
        _onAdClicked?.Invoke();
    }

    private void RestartButtonClicked()
    {
        OnOverlayWindowClosed?.Invoke();
        _onRestartClicked?.Invoke();
        _canvas.enabled = false;
    }

    private void MainMenuButtonClicked()
    {
        _onMenuClicked?.Invoke();
    }
}