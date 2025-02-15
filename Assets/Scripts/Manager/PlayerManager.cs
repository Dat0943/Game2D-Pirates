using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopPlayer
{
    public int price;
    public Sprite hud;
    public PlayerController playerPb;
}

public class PlayerManager : Singleton<PlayerManager>
{
    public ShopPlayer[] players;

    void Start()
    {
        if (players == null || players.Length <= 0) return;

        for (int i = 0; i < players.Length; i++)
        {
            var item = players[i];
            if (item != null)
            {
                // Nếu phần tử đầu tiênn
                if (i == 0)
                {
                    Prefs.SetBool(PrefConsts.PLAYER_PEFIX + i, true);
                }
                else
                {
                    if (!PlayerPrefs.HasKey(PrefConsts.PLAYER_PEFIX + i)) // Nếu dưới máy người dùng chưa có dữ liệu này thì mới cập nhật(chỉ chạy duy nhất lần đầu khi người ta chơi game)
                    {
                        Prefs.SetBool(PrefConsts.PLAYER_PEFIX + i, false);
                    }
                }
            }
        }
    }
}
