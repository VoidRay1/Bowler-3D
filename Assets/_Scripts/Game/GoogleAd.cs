using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class GoogleAd : MonoBehaviour
{
    #if UNITY_ANDROID
        private const string REWARD_AD_UNIT_ID = "ca-app-pub-3940256099942544/5224354917";
        private const string INTERSTITIAL_AD_UNIT_ID = "ca-app-pub-3940256099942544/1033173712";
    #elif UNITY_IPHONE
        private const string REWARD_AD_UNIT_ID = "ca-app-pub-3940256099942544/1712485313";
        private const string INTERSTITIAL_AD_UNIT_ID = "ca-app-pub-3940256099942544/4411468910";
    #endif

    private RewardedAd _rewardedAd;
    private InterstitialAd _interstitialAd;

    private void Start()
    {
        System.Threading.Thread unityThread = System.Threading.Thread.CurrentThread;
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus => { });
        LoadRewardAd();
        LoadInterstitialAd();
    }

    public void LoadRewardAd()
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        AdRequest adRequest = new AdRequest.Builder().AddKeyword("unity-admob-reward").Build();

        RewardedAd.Load(REWARD_AD_UNIT_ID, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                _rewardedAd = ad;
                RegisterReloadHandlerForRewardAd(_rewardedAd);
            });
    }

    public void LoadInterstitialAd()
    {
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        AdRequest adRequest = new 
            AdRequest.Builder().AddKeyword("unity-admob-interstitial").Build();

        InterstitialAd.Load(INTERSTITIAL_AD_UNIT_ID, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _interstitialAd = ad;
                RegisterReloadHandlerForInterstitialAd(_interstitialAd);
            });
    }

    public void ShowRewardedAd(Action<Reward> userRewardEarnedAction)
    {
        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show(userRewardEarnedAction);
        }
    }

    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            _interstitialAd.Show();
        }
    }

    private void RegisterReloadHandlerForRewardAd(RewardedAd ad)
    {
        ad.OnAdFullScreenContentClosed += LoadRewardAd;
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
            LoadRewardAd();
        };
    }

    private void RegisterReloadHandlerForInterstitialAd(InterstitialAd ad)
    {
        ad.OnAdFullScreenContentClosed += LoadInterstitialAd;
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
            LoadInterstitialAd();
        };
    }
}