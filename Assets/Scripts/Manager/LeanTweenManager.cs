using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanTweenManager : Singleton<LeanTweenManager>
{
    [SerializeField] private GameObject ship, piratesImage, coinShopButton, selectWorldButton, soundSettingButton, infoButton, playerSelectButton, itemSupportButton, playButton, goldRewardPanel, addCoinPainel;

    protected override void Awake()
    {
        MakeSingleton(false);
    }

    void Start()
    {
        LeanTween.scale(coinShopButton, Vector3.one, 1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(selectWorldButton, Vector3.one, 1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(soundSettingButton, Vector3.one, 1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(infoButton, Vector3.one, 1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(playerSelectButton, Vector3.one, 1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(itemSupportButton, Vector3.one, 1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(playButton, Vector3.one, 1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(goldRewardPanel, Vector3.one, 1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(addCoinPainel, Vector3.one, 1f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.moveLocal(ship, Vector3.zero, 1f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.moveLocal(piratesImage, new Vector3(0, 545f, 0), 1f).setEase(LeanTweenType.easeInOutCubic);
    }

    public void OpenDialog(GameObject dialog)
    {
        LeanTween.scale(dialog, Vector3.one, 0.5f).setEase(LeanTweenType.easeOutElastic);
    }

    public void CloseDialog(GameObject background)
    {
        LeanTween.scale(background, Vector3.zero, 0.2f).setEase(LeanTweenType.easeInQuad);
    }

    public void BGRotate(GameObject colorWheel)
    {
        LeanTween.rotateAround(colorWheel, Vector3.forward, -360f, 10f).setLoopClamp();
    }
}
