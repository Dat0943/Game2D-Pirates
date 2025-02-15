using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDialog : Dialog
{
    [SerializeField] private Transform gridRoot;
    [SerializeField] private ShopItemUI itemUIPb;

    public override void Show(bool isShow)
    {
        base.Show(isShow);

        UpdateUI();
    }

    void UpdateUI()
    {
        var items = ItemManager.Ins.items;

        if (items == null || items.Length <= 0 || !gridRoot || !itemUIPb) return;

        ClearChilds();

        for (int i = 0; i < items.Length; i++)
        {
            int idx = i;
            var item = items[i];

            // Các câu lệnh hiển thị Dialog
            if (item != null)
            {
                // tạo ra 1 icon mẫu đầu tiên
                ShopItemUI itemUIClone = Instantiate(itemUIPb, Vector3.zero, Quaternion.identity);

                // Setup Layout lưới 
                itemUIClone.transform.SetParent(gridRoot);

                // Reset cho nó về 0
                itemUIClone.transform.localPosition = Vector3.zero;

                itemUIClone.transform.localScale = Vector3.one;

                itemUIClone.UpdateUI(item); // Cập nhật lại tất cả icon

                if (itemUIClone.btn)
                {
                    itemUIClone.btn.onClick.RemoveAllListeners();
                    itemUIClone.btn.onClick.AddListener(() => ItemEvent(item));
                }
            }
        }
    }

    // phương thức xóa tất cả những thằng con của grid khi click nhiều lần
    void ClearChilds()
    {
        if (!gridRoot || gridRoot.childCount < 0) return;

        for (int i = 0; i < gridRoot.childCount; i++)
        {
            var child = gridRoot.GetChild(i);

            if (child)
                Destroy(child.gameObject);
        }
    }

    void ItemEvent(ShopItem item)
    {
        if (item == null) return;

        if (Prefs.CoinData >= item.price)
        {
            if(item.itemName == "Magnet")
            {
                ItemManager.Ins.magnetItemCount++;
                Prefs.MagnetItemData = ItemManager.Ins.magnetItemCount;
            }
            else if(item.itemName == "Immortal")
            {
                ItemManager.Ins.immortalItemCount++;
                Prefs.ImmortalItemData = ItemManager.Ins.immortalItemCount;
            }
            else if(item.itemName == "X2Coins")
            {
                ItemManager.Ins.x2CoinsItemCount++;
                Prefs.X2CoinsItemData = ItemManager.Ins.x2CoinsItemCount;
            }

            GameManager.Ins.CoinCounting -= item.price;
            GameManager.Ins.CoinCounting = Mathf.Clamp(GameManager.Ins.CoinCounting, 0, GameManager.Ins.CoinCounting);
            Prefs.CoinData = GameManager.Ins.CoinCounting;
            GuiManager.Ins.UpdateCoinCounting(GameManager.Ins.CoinCounting);

            UpdateUI();
        }
        else
        {
            Close();
            GuiManager.Ins.ShowOutOfCoinsDialog();
        }
    }
}
