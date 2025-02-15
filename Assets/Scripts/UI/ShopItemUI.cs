using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public TMP_Text priceText;
    public TMP_Text quantityText;
    public Image hud;
    public Button btn;

    public void UpdateUI(ShopItem item)
    {
        if (item == null) return;

        if (hud)
            hud.sprite = item.hud;

        if (priceText)
            priceText.text = item.price.ToString();

        if (item.itemName == "Magnet")
        {
            quantityText.text = ItemManager.Ins.magnetItemCount.ToString();
        }
        else if (item.itemName == "Immortal")
        {
            quantityText.text = ItemManager.Ins.immortalItemCount.ToString();
        }
        else if (item.itemName == "X2Coins")
        {
            quantityText.text = ItemManager.Ins.x2CoinsItemCount.ToString();
        }
    }
}
