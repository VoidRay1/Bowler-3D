using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsWindow : MonoBehaviour, IOverlayWindow
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RootRaycastHandler _rootRaycastHandler;
    [SerializeField] private Button _closeWindowButton;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Transform _settingsImage;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private float _settingsImageDuration;
    [SerializeField] private float _settingsImageSpeed;

    private readonly UnityEvent _onOverlayWindowOpened = new UnityEvent();
    private readonly UnityEvent _onOverlayWindowClosed = new UnityEvent();

    public UnityEvent OnOverlayWindowOpened => _onOverlayWindowOpened;
    public UnityEvent OnOverlayWindowClosed => _onOverlayWindowClosed;

    private void Start()
    {
        _canvasGroup.alpha = 0.0f;
        _canvasGroup.blocksRaycasts = false;
        _volumeSlider.value = GameContext.Instance.SaveData.Volume;
    }

    private void OnEnable()
    {
        _rootRaycastHandler.RegisterOverlayWindowListener(this);
        _volumeSlider.onValueChanged.AddListener(ChangeVolume);
        _closeWindowButton.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        _rootRaycastHandler.UnRegisterOverlayWindowListener(this);
        _volumeSlider.onValueChanged.RemoveListener(ChangeVolume);
        _closeWindowButton.onClick.RemoveListener(Hide);
    }

    private void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
        GameContext.Instance.SaveData.Volume = volume;
        GameContext.Instance.BinarySaveSystem.Save(GameContext.Instance.SaveData);
    }

    public void Show()
    {
        _canvasGroup.blocksRaycasts = true;
        OnOverlayWindowOpened?.Invoke();
        StartCoroutine(RotateSettingsImage(new Vector3(0.0f, 0.0f, 120.0f), _settingsImageDuration));
        StartCoroutine(Fade(1.0f));
    }

    private void Hide() 
    {
        _canvasGroup.blocksRaycasts = false;
        StartCoroutine(RotateSettingsImage(new Vector3(0.0f, 0.0f, -120.0f), _settingsImageDuration));
        StartCoroutine(Fade(0.0f));
        OnOverlayWindowClosed?.Invoke();
    }

    private IEnumerator RotateSettingsImage(Vector3 endValue, float duration)
    {
        float time = 0.0f;
        while(time < duration)
        {
            time += Time.deltaTime;
            _settingsImage.Rotate(endValue * _settingsImageSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Fade(float endValue)
    {
        float time = 0.0f;
        if (_canvasGroup.alpha < endValue)
        {
            while (time < _fadeDuration)
            {
                time += Time.deltaTime;
                _canvasGroup.alpha += Time.deltaTime / _fadeDuration;
                yield return null;
            }
        }
        else
        {
            while (time < _fadeDuration)
            {
                time += Time.deltaTime;
                _canvasGroup.alpha -= Time.deltaTime / _fadeDuration;
                yield return null;
            }
        }
    }
}