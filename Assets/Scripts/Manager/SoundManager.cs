using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip dieSound;
    [SerializeField] private AudioClip sideSound;

    [SerializeField] private Slider bgSoundSlider;
    [SerializeField] private Slider sfxSoundSlider;

    float volumnBgSound;
    float volumnSfxSound;

    public AudioSource bgAudioSource;
    public AudioSource sfxAudioSource;

    protected override void Awake() 
    {
        MakeSingleton(false);
    }

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

    public void PlayCoinSound()
    {
        sfxAudioSource.PlayOneShot(coinSound);
    }

    public void PlayDieSound()
    {
        sfxAudioSource.PlayOneShot(dieSound);
    }

    public void PlaySideSound()
    {
        sfxAudioSource.PlayOneShot(sideSound);
    }

    public void ChangeBgVolume()
    {
        bgAudioSource.volume = bgSoundSlider.value;
        Prefs.BgSoundData = bgSoundSlider.value;
    }

    public void ChangeSFxVolume()
    {
        sfxAudioSource.volume = sfxSoundSlider.value;
        Prefs.SfxSoundData = sfxSoundSlider.value;
    }
}
