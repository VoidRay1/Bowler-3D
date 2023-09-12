using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class PopupWindow : MonoBehaviour, IOverlayWindow
{
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _cancelButton;
    private Canvas _canvas;
    private RootRaycastHandler _rootRaycastHandler;
    private Action _onCancelClicked;
    private Action _onAcceptClicked;
    private readonly UnityEvent _onOverlayWindowOpened = new UnityEvent();
    private readonly UnityEvent _onOverlayWindowClosed = new UnityEvent();

    public UnityEvent OnOverlayWindowOpened => _onOverlayWindowOpened;

    public UnityEvent OnOverlayWindowClosed => _onOverlayWindowClosed;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
    }

    private void OnEnable()
    {
        _acceptButton.onClick.AddListener(AcceptClicked);
        _cancelButton.onClick.AddListener(CancelClicked);
    }

    private void OnDisable()
    {
        _acceptButton.onClick.RemoveListener(AcceptClicked);
        _cancelButton.onClick.RemoveListener(CancelClicked);
        _rootRaycastHandler.UnRegisterOverlayWindowListener(this);
    }

    public void SetRootRaycastHandler(RootRaycastHandler rootRaycastHandler)
    {
        _rootRaycastHandler = rootRaycastHandler;
        _rootRaycastHandler.RegisterOverlayWindowListener(this);
    }

    public void Show(string text, Action onCancelClicked, Action onAcceptClicked, string cancelText = "Cancel", string acceptText = "Accept")
    {
        _titleText.text = text;
        _acceptButton.GetComponentInChildren<TMP_Text>().text = acceptText;
        _cancelButton.GetComponentInChildren<TMP_Text>().text = cancelText;

        OnOverlayWindowOpened?.Invoke();
        _canvas.enabled = true;

        _onCancelClicked = onCancelClicked;
        _onAcceptClicked = onAcceptClicked; 
    }

    private void AcceptClicked()
    {
        _onAcceptClicked?.Invoke();
        _canvas.enabled = false;
    }

    private void CancelClicked()
    {
        OnOverlayWindowClosed?.Invoke();
        _onCancelClicked?.Invoke();
        _canvas.enabled = false;
    }
}