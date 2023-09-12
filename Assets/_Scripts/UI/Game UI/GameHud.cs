using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameHud : MonoBehaviour
{
    [SerializeField] private RootRaycastHandler _rootRaycastHandler;
    [SerializeField] private Button _quitButton;
    private AssetLoader _assetLoader;

    [HideInInspector] public UnityEvent OnQuitGameClicked = new UnityEvent();

    private void Start()
    {
        _assetLoader = new AssetLoader();
    }

    private void OnEnable()
    {
        _quitButton.onClick.AddListener(QuitClicked);
    }

    private void OnDisable()
    {
        _quitButton.onClick.RemoveListener(QuitClicked);
    }

    private async void QuitClicked()
    {
        GameContext.Instance.PauseManager.SetPause(true);
        PopupWindow popupWindow = await _assetLoader.Load<PopupWindow>(Constants.AddressablesIds.POPUP_WINDOW);
        popupWindow.SetRootRaycastHandler(_rootRaycastHandler);
        popupWindow.Show("Are you sure to quit?", ResumeGame, QuitGame, acceptText: "Quit");
    }

    private void ResumeGame()
    {
        _assetLoader.UnloadCachedGameObject();
        GameContext.Instance.PauseManager.SetPause(false);
    }

    private void QuitGame()
    {
        OnQuitGameClicked?.Invoke();
        _assetLoader.UnloadCachedGameObject();
    }
}