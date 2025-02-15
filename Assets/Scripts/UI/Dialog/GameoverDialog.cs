using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverDialog : Dialog
{
    [SerializeField] private TMP_Text timeCountDownText;

    [SerializeField] private float startTimeCountDown = 5f;
    float timeLeft;

    void OnEnable()
    {
        StartCoroutine(CountdownAndRestart());
    }

    IEnumerator CountdownAndRestart()
    {
        timeLeft = startTimeCountDown;

        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(1f);
            timeLeft--;
            timeCountDownText.text = "0" + timeLeft.ToString("0");
        }

        Close();
        GuiManager.Ins.ShowStaticalDialog();
    }

    public void ReplayByCoin()
    {
        if(GameManager.Ins.CoinCounting >= 200)
        {
            if (GameManager.Ins.Player)
                Destroy(GameManager.Ins.Player.gameObject);

            PlayerController newPlayerPb = PlayerManager.Ins.players[Prefs.CurPlayerId].playerPb; 

            if (newPlayerPb)
                GameManager.Ins.Player = Instantiate(newPlayerPb, newPlayerPb.transform.position, Quaternion.identity);

            GameManager.Ins.isDie = false;
            GameManager.Ins.CoinCounting -= 200;
            Prefs.CoinData = GameManager.Ins.CoinCounting;
            Close();
        }
    }

    public void BackHome()
    {
        Close();
        GuiManager.Ins.ShowStaticalDialog();
    }
}