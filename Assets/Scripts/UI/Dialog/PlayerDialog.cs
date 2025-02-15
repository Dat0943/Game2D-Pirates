using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialog : Dialog
{
    [SerializeField] private Transform gridRoot;
    [SerializeField] private ShopPlayerUI playerUIPb;

    public override void Show(bool isShow)
    {
        base.Show(isShow);

        UpdateUI();
    }

    void UpdateUI()
    {
        var players = PlayerManager.Ins.players;

        if (players == null || players.Length <= 0 || !gridRoot || !playerUIPb) return;

        ClearChilds();

        for (int i = 0; i < players.Length; i++)
        {
            int idx = i;
            var player = players[i];

            // Các câu lệnh hiển thị Dialog
            if (player != null)
            {
                // tạo ra 1 icon mẫu đầu tiên
                ShopPlayerUI playerUIClone = Instantiate(playerUIPb, Vector3.zero, Quaternion.identity);

                // Setup Layout lưới 
                playerUIClone.transform.SetParent(gridRoot);

                // Reset cho nó về 0
                playerUIClone.transform.localPosition = Vector3.zero;

                playerUIClone.transform.localScale = Vector3.one;

                playerUIClone.UpdateUI(player, idx); // Cập nhật lại tất cả icon

                if (playerUIClone.btn)
                {
                    playerUIClone.btn.onClick.RemoveAllListeners();
                    playerUIClone.btn.onClick.AddListener(() => PlayerEvent(player, idx));
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

    // Phương thức xử lý các sự kiện khi Click vào các Item trong shop
    void PlayerEvent(ShopPlayer item, int shopItemId)
    {
        if (item == null) return;

        bool isUnlocked = Prefs.GetBool(PrefConsts.PLAYER_PEFIX + shopItemId);

        if (isUnlocked)
        {
            if (shopItemId == Prefs.CurPlayerId) // Khi mà người chơi Cicked vào con đã được mở khóa rồi thì không có gì xảy ra
                return;

            Prefs.CurPlayerId = shopItemId; // Khi người chơi đang click vào thì con đó sẽ được gán lại và đổi sang Active

            UpdateUI(); // Vẽ lại giao diện đó
        }
        else
        {
            if (Prefs.CoinData >= item.price)
            {
                GameManager.Ins.CoinCounting -= item.price;
                Prefs.CoinData = GameManager.Ins.CoinCounting;

                Prefs.SetBool(PrefConsts.PLAYER_PEFIX + shopItemId, true); // Mở khóa Player

                Prefs.CurPlayerId = shopItemId; // Khi người chơi đang click vào thì con đó sẽ được gán lại và đổi sang Active

                GuiManager.Ins.UpdateCoinCounting(GameManager.Ins.CoinCounting); // Cập nhật Coins

                UpdateUI(); // Vẽ lại giao diện đó
            }
            else
            {
                Close();
                GuiManager.Ins.ShowOutOfCoinsDialog();
            }
        }
    }
}
