using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldRewardDialog : Dialog
{
    [SerializeField] private GameObject colorWheel;

    void OnEnable()
    {
        LeanTweenManager.Ins.BGRotate(colorWheel);
        StartCoroutine(CountdownAndBackHome());   
    }

    IEnumerator CountdownAndBackHome()
    {
        yield return new WaitForSeconds(3f);
        BackHome();
    }

    public void BackHome()
    {
        Close();
    }
}
