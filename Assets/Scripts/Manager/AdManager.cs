using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    [SerializeField] private string rewardId = "ca-app-pub-2007871052832732/2241917624";

    BannerView bannerView;
    InterstitialAd interstitialAd;
    RewardedAd rewardedAd;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
#if UNITY_IOS
		rewardId = "ca-app-pub-2007871052832732/2241917624";
#elif UNITY_ANDROID
        rewardId = "ca-app-pub-2007871052832732/2241917624";
#endif
        rewardId = "ca-app-pub-2007871052832732/2241917624";

        MobileAds.Initialize(initStatus =>
        {

        });

        LoadRewardAds();
    }

    #region RewardAD
    void LoadRewardAds()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Load Reward Ads");

        var adRequest = new AdRequest();
        RewardedAd.Load(rewardId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.Log("RewardAds error");
                return;
            }
            rewardedAd = ad;
        });
    }

    public void ShowRewardAds()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                HandleUserEarnedReward(reward);
            });
        }
    }

    void HandleUserEarnedReward(Reward reward)
    {
        int coinsReward = 50; 
        Debug.Log("Người chơi nhận được " + coinsReward + " xu!");
        GameManager.Ins.CoinCounting += coinsReward;
        Prefs.CoinData = GameManager.Ins.CoinCounting;
        GuiManager.Ins.UpdateCoinCounting(Prefs.CoinData);
        PlayerPrefs.Save();
    }

    public void ShowRewardAdsAndReplayGame()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                HandleReplayGameByAds();
            });
        }
    }

    void HandleReplayGameByAds()
    {
        if (GameManager.Ins.Player)
            Destroy(GameManager.Ins.Player.gameObject);

        PlayerController newPlayerPb = PlayerManager.Ins.players[Prefs.CurPlayerId].playerPb;

        if (newPlayerPb)
            GameManager.Ins.Player = Instantiate(newPlayerPb, newPlayerPb.transform.position, Quaternion.identity);

        GameManager.Ins.isDie = false;
    }
    #endregion
}
