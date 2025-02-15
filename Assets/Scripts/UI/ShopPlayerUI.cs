using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPlayerUI : MonoBehaviour
{
    public TMP_Text priceText;
    public Image hud;
    public Image active;
    public Image coin;
    public Button btn;

    public void UpdateUI(ShopPlayer item, int shopItemId)
    {
        if (item == null) return;

        if (hud)
            hud.sprite = item.hud;

        // Lấy dữ liệu id người chơi từ máy người dùng xuống 
        bool isUnlocked = Prefs.GetBool(PrefConsts.PLAYER_PEFIX + shopItemId);

        if (isUnlocked)
        {
            coin.gameObject.SetActive(false);

            if (priceText)
                priceText.text = "Unlocked";

            if (shopItemId == Prefs.CurPlayerId)
                active.gameObject.SetActive(true);
            else
                active.gameObject.SetActive(false);
        }
        else
        {
            if (priceText)
                priceText.text = item.price.ToString();
        }
    }
}
