using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private BallPreview _swipeAreaBallPreview;
    [SerializeField] private BallsCollectionWindow _ballsCollectionWindow;
    [SerializeField] private AchievementsWindow _achievementsWindow;
    [SerializeField] private SettingsWindow _settingsWindow;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _startGameButton;

    private void Start()
    {
        _swipeAreaBallPreview.Display(GameContext.Instance.SelectedBall);
    }

    private void OnEnable()
    {
        _settingsButton.onClick.AddListener(ShowSettingsWindow);
        _startGameButton.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        _settingsButton.onClick.RemoveListener(ShowSettingsWindow);
        _startGameButton.onClick.RemoveListener(StartGame);
    }

    private void ShowSettingsWindow()
    {
        _settingsWindow.Show();
    }

    private void StartGame()
    {
        SceneManager.LoadSceneAsync(Constants.Scenes.GAME_SCENE, LoadSceneMode.Single);
    }
}