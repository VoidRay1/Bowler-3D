using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Events;

public class AdProvider : MonoBehaviour
{
    [SerializeField] private int _countToShowInterstitialAd = 2;
    [SerializeField] private GoogleAd _googleAd;
    [SerializeField] private Player _player;

    private bool _isRewardedAdShown = false;
    private int _currentCountToShowInterstitialAd;

    public bool IsRewardedAdSwown => _isRewardedAdShown;
    [HideInInspector] public UnityEvent OnRewardAddedToUser = new UnityEvent();

    private void Start()
    {
        _currentCountToShowInterstitialAd = 0;
    }

    public void Reset()
    {
        _isRewardedAdShown = false;
    }

    public void TryShowInterstitialAd()
    {
        if(_currentCountToShowInterstitialAd == _countToShowInterstitialAd)
        {
            _googleAd.ShowInterstitialAd();
            _currentCountToShowInterstitialAd = 0;
        }
        else
        {
            _currentCountToShowInterstitialAd++;
        }
    }

    public void ShowRewardedAd()
    {
        _googleAd.ShowRewardedAd(AddRewardToUser);
    }

    private void AddRewardToUser(Reward reward)
    {
        _player.AddAttempts(3); // change for deploy
        _isRewardedAdShown = true;
        OnRewardAddedToUser?.Invoke();
    }
}