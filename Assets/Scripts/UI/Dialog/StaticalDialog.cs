using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticalDialog : Dialog
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    void OnEnable()
    {
        coinText.text = GameManager.Ins.CoinInGame.ToString();
        scoreText.text = GameManager.Ins.GetDistanceClimbed().ToString("0") + "m";

        if(GameManager.Ins.GetDistanceClimbed() > Prefs.HighScoreData)
        {
            Prefs.HighScoreData = GameManager.Ins.GetDistanceClimbed();
            highScoreText.text = "BEST DISTANCE : " + GameManager.Ins.GetDistanceClimbed() + "m";
        }
        highScoreText.text = "BEST DISTANCE : " + Prefs.HighScoreData.ToString() + "m";

        StartCoroutine(CountdownAndBackHome());
    }

    IEnumerator CountdownAndBackHome()
    {
        GameManager.Ins.isStart = false;
        yield return new WaitForSeconds(3f);
        BackHome();
    }

    void BackHome()
    {
        Close();
        ReloadCurrentScene();
    }

    void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
