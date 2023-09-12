using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LocalesWindow : MonoBehaviour, IOverlayWindow, IPointerClickHandler, ISelectHandler
{
    [SerializeField] private RootRaycastHandler _rootRaycastHandler;

    private readonly UnityEvent _onOverlayWindowOpened = new UnityEvent();
    private readonly UnityEvent _onOverlayWindowClosed = new UnityEvent();

    public UnityEvent OnOverlayWindowOpened => _onOverlayWindowOpened;
    public UnityEvent OnOverlayWindowClosed => _onOverlayWindowClosed;

    private void OnEnable()
    {
        _rootRaycastHandler.RegisterOverlayWindowListener(this);
    }

    private void OnDisable()
    {
        _rootRaycastHandler.UnRegisterOverlayWindowListener(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnOverlayWindowOpened?.Invoke();
    }

    public void OnSelect(BaseEventData eventData)
    {
        OnOverlayWindowClosed?.Invoke();
    }
}
