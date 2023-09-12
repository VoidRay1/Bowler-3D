using UnityEngine;

public class InputActions : MonoBehaviour
{
    private PlayerInputs _actions;

    public PlayerInputs Actions => _actions;
    public static InputActions Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            _actions = new PlayerInputs();
        }
    }

    private void OnEnable()
    {
        _actions.Enable();
    }

    private void OnDisable()
    {
        _actions.Disable(); 
    }
}