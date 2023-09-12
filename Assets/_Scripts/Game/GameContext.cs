using UnityEngine;
using UnityEngine.EventSystems;

public class GameContext : MonoBehaviour
{
    [SerializeField] private BallUI _fallbackBall;
    [SerializeField] private BallsList _ballsList;
    [SerializeField] private AchievementsList _achievementsList;
    [SerializeField] private DestructableObjectFactory _destructableObjectFactory;
    [SerializeField] private SoundInteractableFactory _soundInteractableFactory;
    [SerializeField] private SoundPlayer _soundPlayer;
    [SerializeField] private EventSystem _eventSystem;

    private AchievementsHandler _achievementsHandler;
    private AchievementPopupMessageProvider _achievementPopupMessageProvider;

    public BallUI SelectedBall;
    [HideInInspector] public SaveData SaveData;
    public BallUI FalbackBall => _fallbackBall;
    public BallsList BallsList => _ballsList;
    public AchievementsList AchievementsList => _achievementsList;
    public DestructableObjectFactory DestructableObjectFactory => _destructableObjectFactory;
    public SoundInteractableFactory SoundInteractableFactory => _soundInteractableFactory;
    public SoundPlayer SoundPlayer => _soundPlayer;
    public EventSystem EventSystem => _eventSystem;
    public LoadingScreenProvider LoadingScreenProvider { get; private set; }
    public PauseManager PauseManager { get; private set; }
    public BinarySaveSystem BinarySaveSystem { get; private set; }
    public static GameContext Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void Initialize()
    {
        PauseManager = new PauseManager();
        BinarySaveSystem = new BinarySaveSystem();
        LoadingScreenProvider = new LoadingScreenProvider();
        _achievementPopupMessageProvider = new AchievementPopupMessageProvider();
        _achievementsHandler = new AchievementsHandler(_achievementsList, _achievementPopupMessageProvider);
        SaveData = BinarySaveSystem.Load();
        _soundPlayer.Init();
    }
}