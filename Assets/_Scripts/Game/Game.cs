using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private ScoreMultiplierHandler _scoreMultiplierHandler;
    [SerializeField] private ScoreSystem _scoreSystem;
    [SerializeField] private DestructibleObjectSpawner _destructibleObjectSpawner;
    [SerializeField] private BallSpawner _ballSpawner;
    [SerializeField] private Player _player;
    [SerializeField] private LevelCompleteChecker _levelCompleteChecker;
    [SerializeField] private LevelCreator _levelCreator;
    [SerializeField] private AdProvider _adProvider;
    [SerializeField] private RootRaycastHandler _rootRaycastHandler;

    [SerializeField] private GameHud _gameHud;
    [SerializeField] private LevelDisplayer _levelDisplayer;
    [SerializeField] private RespawnBallUI _respawnBallUI;

    [SerializeField] private int _countToUpLevel = 5;

    private AssetLoader _assetLoader;
    private Difficulty _difficulty;
    private int _currentLevelNumber;
    private SaveData SaveData => GameContext.Instance.SaveData;

    [HideInInspector] public static UnityEvent<int> OnLevelCreated = new UnityEvent<int>();


    private void Start()
    {
        _assetLoader = new AssetLoader();
        BeginNewGame();
    }

    private void Update()
    {
        _scoreSystem.SelfUpdate();
        _scoreMultiplierHandler.SelfUpdate();
    }

    private void OnEnable()
    {
        _gameHud.OnQuitGameClicked.AddListener(QuitGame);
        _adProvider.OnRewardAddedToUser.AddListener(ResumeGame);
        _levelCompleteChecker.OnLevelCompleted.AddListener(CreateNextLevel);
        _respawnBallUI.OnRespawnBallButtonClicked.AddListener(TryRespawnBall);
    }

    private void OnDisable()
    {
        _gameHud.OnQuitGameClicked.RemoveListener(QuitGame);
        _adProvider.OnRewardAddedToUser.RemoveListener(ResumeGame);
        _levelCompleteChecker.OnLevelCompleted.RemoveListener(CreateNextLevel);
        _respawnBallUI.OnRespawnBallButtonClicked.RemoveListener(TryRespawnBall);
    }

    private void BeginNewGame()
    {
        _assetLoader.UnloadCachedGameObject();
        _adProvider.Reset();
        _adProvider.TryShowInterstitialAd();
        _difficulty = Difficulty.Easy;
        _currentLevelNumber = 0;
        _scoreSystem.Reset();
        _scoreMultiplierHandler.Reset();
        CreateNextLevel();
        GameContext.Instance.PauseManager.SetPause(false);
    }

    private void ResumeGame()
    {
        _assetLoader.UnloadCachedGameObject();
        TryRespawnBall();
        GameContext.Instance.PauseManager.SetPause(false);
    }

    private void QuitGame()
    {
        SceneManager.LoadSceneAsync(Constants.Scenes.MAIN_MENU_SCENE);
    }

    private void CreateNextLevel()
    {
        _currentLevelNumber++;
        TrySwitchDifficulty();
        _levelCreator.CreateNextLevel(_difficulty);
        _player.AddAttempts(_destructibleObjectSpawner.SpawnPattern.AttemptsCount);
        TryRespawnBall();
        _levelDisplayer.DisplayLevel(_currentLevelNumber);
        OnLevelCreated?.Invoke(_currentLevelNumber);
    }

    private async void TryRespawnBall()
    {
        GameContext.Instance.SoundPlayer.StopSound();
        if (_player.BallsLeft == 0)
        {
            GameContext.Instance.PauseManager.SetPause(true);
            if(SaveData.MaxScore < _scoreSystem.CurrentScore)
            {
                SaveData.MaxScore = _scoreSystem.CurrentScore;
                GameContext.Instance.BinarySaveSystem.Save(SaveData);
            }
            LoseWindow loseWindow = await _assetLoader.Load<LoseWindow>(Constants.AddressablesIds.LOSE_WINDOW);
            loseWindow.SetRootRaycastHandler(_rootRaycastHandler);
            loseWindow.ShowWindow(_scoreSystem.CurrentScore, SaveData.MaxScore, QuitGame, BeginNewGame, ShowRewardAd, !_adProvider.IsRewardedAdSwown);
        }
        else
        {
            _ballSpawner.SpawnBall();
            _player.DecreaseAttempts();
            _respawnBallUI.ChangeRespawnButtonText(Constants.LocalizationVariables.RESPAWN_BALL);
            if (_player.BallsLeft == 0)
            {
                _respawnBallUI.ChangeRespawnButtonText(Constants.LocalizationVariables.SHOW_RESULTS);
            }
        }
    }

    private void ShowRewardAd()
    {
       _adProvider.ShowRewardedAd();
    }

    private void TrySwitchDifficulty()
    {
        if (_currentLevelNumber % _countToUpLevel == 0)
        {
            switch (_difficulty)
            {
                case Difficulty.Easy:
                    _difficulty = Difficulty.Medium;
                    break;
                case Difficulty.Medium:
                    _difficulty = Difficulty.Hard;
                    break;
                case Difficulty.Hard:
                    _difficulty = Difficulty.Impossible;
                    break;
                case Difficulty.Impossible:
                    break;
            }
        }
    }
}