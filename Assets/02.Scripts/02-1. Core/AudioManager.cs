using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource _sfxAudioSource;
    private AudioSource _bgmAudioSource;
    [SerializeField]
    private AudioClip _bgmClip;

    protected override void Awake()
    {
        base.Awake();
        // 오디오 소스 생성
        GameObject SFX = new GameObject("SFX");
        SFX.transform.SetParent(transform);
        _sfxAudioSource = SFX.AddComponent<AudioSource>();
        _sfxAudioSource.playOnAwake = false;
        _sfxAudioSource.loop = false;
        
        GameObject BGM = new GameObject("BGM");
        BGM.transform.SetParent(transform);
        _bgmAudioSource = BGM.AddComponent<AudioSource>();
        _bgmAudioSource.playOnAwake = false;
        _bgmAudioSource.loop = true;
        PlayBGM(_bgmClip);
        SetBGMVolume(0.25f);
    }

    public void SetSFXVolume(float volume)
    {
        _sfxAudioSource.volume = volume;
    }

    public void SetBGMVolume(float volume)
    {
        _bgmAudioSource.volume = volume;
    }

    public void PlayBGM(AudioClip clip)
    {
        _bgmAudioSource.clip = clip;
        _bgmAudioSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        _sfxAudioSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip, float delay)
    {
        StartCoroutine(DelayPlaySFX(clip, delay));
    }

    private IEnumerator DelayPlaySFX(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        _sfxAudioSource.PlayOneShot(clip);
    }
}