using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RootRaycastHandler : MonoBehaviour
{
    [SerializeField] private GraphicRaycaster _graphicRaycaster;

    private readonly List<IOverlayWindow> _overlayWindows = new List<IOverlayWindow>();

    public void RegisterOverlayWindowListener(IOverlayWindow window)
    {
        _overlayWindows.Add(window);
        SubscribeOnOverlayWindowEvents(window);
    }

    public void UnRegisterOverlayWindowListener(IOverlayWindow window)
    {
        _overlayWindows.Remove(window);
        UnSubscribeOnOverlayWindowsEvents(window);
    }

    private void SubscribeOnOverlayWindowEvents(IOverlayWindow window)
    {
        window.OnOverlayWindowOpened.AddListener(DisableRootRaycast);
        window.OnOverlayWindowClosed.AddListener(EnableRootRaycast);
    }

    private void UnSubscribeOnOverlayWindowsEvents(IOverlayWindow window)
    {
        window.OnOverlayWindowOpened.RemoveListener(DisableRootRaycast);
        window.OnOverlayWindowClosed.RemoveListener(EnableRootRaycast);
    }

    private void EnableRootRaycast()
    {
        _graphicRaycaster.enabled = true;
    }

    private void DisableRootRaycast()
    {
        _graphicRaycaster.enabled = false;
    }
}
