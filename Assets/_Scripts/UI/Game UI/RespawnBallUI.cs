using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.UI;

public class RespawnBallUI : MonoBehaviour
{
    [SerializeField] private float _fadeDuration;
    [SerializeField] private TMP_Text _textToFade;
    [SerializeField] private Animator _animator;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _respawnButton;
    [SerializeField] private LocalizedString _localizedRespawnText;
    private int _isOpenedHash;
    private bool _fadeInProcess;

    [HideInInspector] public UnityEvent OnRespawnBallButtonClicked = new UnityEvent();

    private void Start()
    {
        _isOpenedHash = Animator.StringToHash("IsOpened");
        Hide();
    }

    private void OnEnable()
    {
        _localizedRespawnText.StringChanged += RespawnButtonTextChanged;
        _respawnButton.onClick.AddListener(RespawnBall);
    }

    private void OnDisable()
    {
        _localizedRespawnText.StringChanged -= RespawnButtonTextChanged;
        _respawnButton.onClick.RemoveListener(RespawnBall);
    }

    public void Show()
    {
        _canvas.enabled = true;
        _animator.SetBool(_isOpenedHash, true);
    }

    public void Hide()
    {
        _canvas.enabled = false;
        _animator.SetBool(_isOpenedHash, false);
    }

    public void ChangeRespawnButtonText(string stringReference)
    {
        _localizedRespawnText.TableEntryReference = stringReference;
    }

    private void RespawnButtonTextChanged(string text)
    {
        _respawnButton.GetComponentInChildren<TMP_Text>().text = text;
    }

    private void RespawnBall()
    {
        OnRespawnBallButtonClicked?.Invoke();
        Hide();
    }

    public void BallStateChanged(BallBaseState state)
    {
        if(state is MotionState) 
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    /*private IEnumerator ShowAndHide()
    {
        while(_canvas.enabled && _fadeInProcess == false)
        {
            print("Coroutine");
            _fadeInProcess = true;
            _textToFade.alpha = 0.7f;

            float time = 0.0f;
            while (time < _fadeDuration)
            {
                _textToFade.alpha += Time.deltaTime / _fadeDuration;
                print(_textToFade.alpha);
                time += Time.deltaTime;
                yield return null;
            }

            _textToFade.alpha = 1;

            time = 0.0f;
            while (time < _fadeDuration)
            {
                _textToFade.alpha -= Time.deltaTime / _fadeDuration;
                print(_textToFade.alpha);
                time += Time.deltaTime;
                yield return null;
            }

            _textToFade.alpha = 0.7f;
            _fadeInProcess = false;
        }
    }*/
}