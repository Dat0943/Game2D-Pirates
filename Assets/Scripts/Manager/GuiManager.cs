using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : Singleton<GuiManager>
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;

    [SerializeField] private TMP_Text coinInGameText;
    [SerializeField] private TMP_Text coinCountingText;

    [SerializeField] private Image immortalBarImage;
    [SerializeField] private Image magnetBarImage;
    [SerializeField] private Image x2CoinBarImage;

    [SerializeField] private Dialog gameoverDialog;
    [SerializeField] private Dialog staticalDialog;
    [SerializeField] private Dialog outOfCoinsDialog;

    [SerializeField] private GameObject bgGameoverDialog;
    [SerializeField] private GameObject bgStaticalDialog;
    [SerializeField] private GameObject bgOutOfCoinsDialog;

    Dialog activeDialog;
    public Dialog ActiveDialog { get => activeDialog; private set => activeDialog = value; }

    protected override void Awake()
    {
        MakeSingleton(false);
    }

    public void ShowGamePanel(bool isShow)
    {
        menuPanel.SetActive(!isShow);
        gamePanel.SetActive(isShow);
    }

    public void ShowDialog(Dialog dialog)
    {
        if (dialog == null) return;

        activeDialog = dialog;
        activeDialog.Show(true);
    }

    public void ShowGameoverDialog()
    {
        ShowDialog(gameoverDialog);
        LeanTweenManager.Ins.OpenDialog(bgGameoverDialog);
    }

    public void ShowStaticalDialog()
    {
        ShowDialog(staticalDialog);
        LeanTweenManager.Ins.OpenDialog(bgStaticalDialog);
    }

    public void ShowOutOfCoinsDialog()
    {
        ShowDialog(outOfCoinsDialog);
        LeanTweenManager.Ins.OpenDialog(bgOutOfCoinsDialog);
    }

    public void UpdateCoinInGame(int coin)
    {
        coinInGameText.text = coin.ToString();
    }

    public void UpdateCoinCounting(int coin)
    {
        coinCountingText.text = coin.ToString();
    }

    public void ShowImmortalBar(bool show)
    {
        immortalBarImage.gameObject.SetActive(show);
    }

    public void UpdateImmortalBar(float fillAmount)
    {
        immortalBarImage.fillAmount = fillAmount;
    }

    public void ShowMagnetBar(bool show)
    {
        magnetBarImage.gameObject.SetActive(show);
    }

    public void UpdateMagnetBar(float fillAmount)
    {
        magnetBarImage.fillAmount = fillAmount;
    }

    public void ShowX2CoinsBar(bool show)
    {
        x2CoinBarImage.gameObject.SetActive(show);
    }

    public void UpdateX2CoinsBar(float fillAmount)
    {
        x2CoinBarImage.fillAmount = fillAmount;
    }
}
