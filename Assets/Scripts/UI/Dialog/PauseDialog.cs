using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseDialog : Dialog
{
    [SerializeField] private Slider bgSoundSlider;
    [SerializeField] private Slider sfxSoundSlider;

    void Start()
    {
        if (!PlayerPrefs.HasKey(PrefConsts.BG_SOUND))
        {
            Prefs.BgSoundData = 1;
            Prefs.SfxSoundData = 1;
            bgSoundSlider.value = Prefs.BgSoundData;
            sfxSoundSlider.value = Prefs.SfxSoundData;
        }
        else
        {
            bgSoundSlider.value = Prefs.BgSoundData;
            sfxSoundSlider.value = Prefs.SfxSoundData;
        }
    }

    public void OpenPauseDialog()
    {
        GuiManager.Ins.ShowDialog(this);
        LeanTweenManager.Ins.OpenDialog(background);
        StartCoroutine(PauseTime());
    }

    IEnumerator PauseTime()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Ins.PauseGame();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Close();
    }

    public void BackHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeBgVolume()
    {
        SoundManager.Ins.bgAudioSource.volume = bgSoundSlider.value;
        Prefs.BgSoundData = bgSoundSlider.value;
    }

    public void ChangeSFxVolume()
    {
        SoundManager.Ins.sfxAudioSource.volume = sfxSoundSlider.value;
        Prefs.SfxSoundData = sfxSoundSlider.value;
    }
}
