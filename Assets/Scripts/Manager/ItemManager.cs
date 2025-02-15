using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class ShopItem
{
    public string itemName;
    public int price;
    public Sprite hud;
}

public class ItemManager : Singleton<ItemManager>
{
    public ShopItem[] items;
    public int magnetItemCount;
    public int immortalItemCount;
    public int x2CoinsItemCount;

    [SerializeField] private TMP_Text magnetItemCountText;
    [SerializeField] private TMP_Text immortalItemCountText;
    [SerializeField] private TMP_Text x2CoinsItemCountText;

    protected override void Awake()
    {
        MakeSingleton(false);
    }

    void Start()
    {
        magnetItemCount = Prefs.MagnetItemData;
        immortalItemCount = Prefs.ImmortalItemData;
        x2CoinsItemCount = Prefs.X2CoinsItemData;
        UpdateItemText();
    }

    public void UpdateItemText()
    {
        magnetItemCountText.text = magnetItemCount.ToString();
        immortalItemCountText.text = immortalItemCount.ToString();
        x2CoinsItemCountText.text = x2CoinsItemCount.ToString();
    }

    public void UseMagnet()
    {
        if (magnetItemCount > 0 && GameManager.Ins.Player.IsMagnetMode == false)
        {
            magnetItemCount--;
            Prefs.MagnetItemData = magnetItemCount;
            UpdateItemText();

            StartCoroutine(UseMagnetItem(5f));
        }
        else
        {
            Debug.Log("Chưa đủ");
        }
    }

    IEnumerator UseMagnetItem(float duration)
    {
        float timer = 0f;
        GameManager.Ins.Player.MagnetMode(true);
        GuiManager.Ins.ShowMagnetBar(true);

        while (timer < duration)
        {
            GuiManager.Ins.UpdateMagnetBar(1 - (timer / duration));

            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }

        GameManager.Ins.Player.MagnetMode(false);
        GuiManager.Ins.ShowMagnetBar(false);
    }

    public void UseImmortal()
    {
        if (immortalItemCount > 0 && GameManager.Ins.Player.IsImmortalMode == false)
        {
            immortalItemCount--;
            Prefs.ImmortalItemData = immortalItemCount;
            UpdateItemText();

            StartCoroutine(UseImmortalItem(5f));
        }
        else
        {
            Debug.Log("Chưa đủ");
        }
    }

    IEnumerator UseImmortalItem(float duration)
    {
        float timer = 0f;
        bool isRed = false;

        GameManager.Ins.Player.ImmortalMode(true);
        GuiManager.Ins.ShowImmortalBar(true);

        while (timer < duration)
        {
            GameManager.Ins.Player.GetComponent<SpriteRenderer>().color = isRed ? Color.red : Color.white;
            isRed = !isRed;

            GuiManager.Ins.UpdateImmortalBar(1 - (timer / duration));

            yield return new WaitForSeconds(0.1f); // Thời gian chuyển đổi màu
            timer += 0.1f;
        }

        GameManager.Ins.Player.GetComponent<SpriteRenderer>().color = Color.white; 
        GameManager.Ins.Player.ImmortalMode(false);
        GuiManager.Ins.ShowImmortalBar(false);
    }

    public void UseX2Coins()
    {
        if(x2CoinsItemCount > 0 && GameManager.Ins.Player.IsX2CoinMode == false)
        {
            x2CoinsItemCount--;
            Prefs.X2CoinsItemData = x2CoinsItemCount;
            UpdateItemText();

            StartCoroutine(UseX2CoinsItem(5f));
        }
        else
        {
            Debug.Log("Chưa đủ");
        }
    }

    IEnumerator UseX2CoinsItem(float duration)
    {
        float timer = 0f;
        GameManager.Ins.Player.X2CoinMode(true);
        GuiManager.Ins.ShowX2CoinsBar(true);

        while (timer < duration)
        {
            GuiManager.Ins.UpdateX2CoinsBar(1 - (timer / duration));

            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }

        GameManager.Ins.Player.X2CoinMode(false);
        GuiManager.Ins.ShowX2CoinsBar(false);
    }
}
