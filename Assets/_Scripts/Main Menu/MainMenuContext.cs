using UnityEngine;

public class MainMenuContext : MonoBehaviour
{
    [SerializeField] private RootRaycastHandler _rootRaycastHandler;

    public RootRaycastHandler RootRaycastHandler => _rootRaycastHandler;
    public static MainMenuContext Instance { get; private set; }

    private void Awake()
    {
        Instance = this; 
    }
}