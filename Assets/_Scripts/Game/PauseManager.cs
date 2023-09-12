using System.Collections.Generic;

public class PauseManager : IPauseHandler
{
    private readonly List<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();

    public bool IsPaused { get; private set;} 

    public void RegisterPauseListener(IPauseHandler pauseHandler)
    {
        _pauseHandlers.Add(pauseHandler);
    }

    public void UnRegisterPauseListener(IPauseHandler pauseHandler)
    {
        _pauseHandlers.Remove(pauseHandler);
    }

    public void SetPause(bool isPaused)
    {
        IsPaused = isPaused;
        foreach(IPauseHandler handler in _pauseHandlers)
        {
            handler.SetPause(isPaused);
        }
    }
}