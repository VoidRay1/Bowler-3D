using UnityEngine.Events;

public interface IOverlayWindow
{ 
    UnityEvent OnOverlayWindowOpened { get; }
    UnityEvent OnOverlayWindowClosed { get; }
}