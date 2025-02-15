using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefs
{
    public static int CoinData
    {
        set => PlayerPrefs.SetInt(PrefConsts.COIN, value);
        get => PlayerPrefs.GetInt(PrefConsts.COIN, 0);
    }

    public static int HighScoreData
    {
        set => PlayerPrefs.SetInt(PrefConsts.HIGHSCORE, value);
        get => PlayerPrefs.GetInt(PrefConsts.HIGHSCORE, 0);
    }

    public static string GiftTimeData
    {
        set => PlayerPrefs.SetString(PrefConsts.NEXTGIFTTIME, value);
        get => PlayerPrefs.GetString(PrefConsts.NEXTGIFTTIME, DateTime.Now.AddHours(-12).ToBinary().ToString());
    }

    public static int MapData
    {
        set => PlayerPrefs.SetInt(PrefConsts.SELECTEDMAP, value);
        get => PlayerPrefs.GetInt(PrefConsts.SELECTEDMAP, 1);
    }

    #region Player
    public static int CurPlayerId
    {
        set => PlayerPrefs.SetInt(PrefConsts.CUR_PLAYER_ID, value);
        get => PlayerPrefs.GetInt(PrefConsts.CUR_PLAYER_ID);
    }

    // Phương thức lưu dữ liệu xuống máy người dùng (mở khóa Player)
    public static void SetBool(string key, bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt(key, 1); // Lưu dữ liệu xuống máy người dùng dùng
        }
        else
        {
            PlayerPrefs.SetInt(key, 0);
        }
    }


    // Phương thức lấy dữ liệu từ máy người dùng
    public static bool GetBool(string key)
    {
        return PlayerPrefs.GetInt(key) == 1 ? true : false;
    }
    #endregion

    #region Item
    public static int MagnetItemData
    {
        set => PlayerPrefs.SetInt(PrefConsts.MAGNET_ITEM, value);
        get => PlayerPrefs.GetInt(PrefConsts.MAGNET_ITEM);
    }

    public static int ImmortalItemData
    {
        set => PlayerPrefs.SetInt(PrefConsts.IMMORTAL_ITEM, value);
        get => PlayerPrefs.GetInt(PrefConsts.IMMORTAL_ITEM);
    }

    public static int X2CoinsItemData
    {
        set => PlayerPrefs.SetInt(PrefConsts.X2COINS_ITEM, value);
        get => PlayerPrefs.GetInt(PrefConsts.X2COINS_ITEM);
    }
    #endregion

    public static float SfxSoundData
    {
        set => PlayerPrefs.SetFloat(PrefConsts.SFX_SOUND, value);
        get => PlayerPrefs.GetFloat(PrefConsts.SFX_SOUND, 1);
    }

    public static float BgSoundData
    {
        set => PlayerPrefs.SetFloat(PrefConsts.BG_SOUND, value);
        get => PlayerPrefs.GetFloat(PrefConsts.BG_SOUND, 1);
    }
}
