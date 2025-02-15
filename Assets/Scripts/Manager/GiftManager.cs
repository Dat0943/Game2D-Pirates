using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GiftManager : MonoBehaviour
{
    [SerializeField] private TMP_Text giftTimerText;
    [SerializeField] private Button rewardButton;
    DateTime nextGiftTime;

    void Start()
    {
        nextGiftTime = DateTime.FromBinary(Convert.ToInt64(Prefs.GiftTimeData));
        UpdateGiftTimer();
    }

    void Update()
    {
        UpdateGiftTimer();
    }

    void UpdateGiftTimer()
    {
        TimeSpan remainingTime = nextGiftTime - DateTime.Now;
        if (remainingTime.TotalSeconds > 0)
        {
            giftTimerText.text = remainingTime.Hours.ToString("00") + ":" + remainingTime.Minutes.ToString("00");
            rewardButton.interactable = false;
        }
        else
        {
            giftTimerText.text = "00:00";
            rewardButton.interactable = true;
        }
    }

    public void ClaimGift()
    {
        if (DateTime.Now >= nextGiftTime)
        {
            GameManager.Ins.AddCoin(200);
            GuiManager.Ins.UpdateCoinCounting(GameManager.Ins.CoinCounting);

            // Thiết lập lại thời gian chờ 12 giờ
            nextGiftTime = DateTime.Now.AddHours(12);
            Prefs.GiftTimeData = nextGiftTime.ToBinary().ToString();
            UpdateGiftTimer();
        }
    }
}
