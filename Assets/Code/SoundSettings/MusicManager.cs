using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class MusicManager : SingletonMonoBehaviour<MusicManager>
{

    private AudioSource audioSource = null;
    private AudioClip currentAudioClip = null;
    private Coroutine fadeOutMusicCoroutine;
    private Coroutine fadeInMusicCoroutine;

    [SerializeField]private int musicVolume = 10;
  

    public int MusicVolume { get => musicVolume; set => musicVolume = value; }

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();

        HelperUtilities.GameResources.musicOffSnapshot.TransitionTo(0f);
    }

    private void Start()
    {
        SetMusicVolume(musicVolume);
    }

    private void SetMusicVolume(int musicVolume)
    {
        float muteDecibels = -80f;

        if (musicVolume==0)
            HelperUtilities.GameResources.musicMixerGroup.audioMixer.SetFloat("musicVolume", muteDecibels);
        
        else
            HelperUtilities.GameResources.musicMixerGroup.audioMixer.SetFloat("musicVolume", HelperUtilities.LinearToDecibels(musicVolume));
    }

    public void PlayMusic(MusicTrackSO musicTrack, float fadeOutTime = Settings.musicFadeOutTime, float fadeInTime = Settings.musicFadeInTime)
    {
        StartCoroutine(PlayMusicRoutine(musicTrack, fadeOutTime, fadeInTime));

    }

    private IEnumerator PlayMusicRoutine(MusicTrackSO musicTrack, float fadeOutTime, float fadeInTime)
    {
        if (fadeOutMusicCoroutine !=null)
        {
            StopCoroutine(fadeOutMusicCoroutine);
        }
        if (fadeInMusicCoroutine!= null)
        {
            StopCoroutine(fadeInMusicCoroutine);
        }

        if (musicTrack.MusicClip!=currentAudioClip)
        {
            currentAudioClip = musicTrack.MusicClip;
            yield return fadeOutMusicCoroutine = StartCoroutine(FadeOutMusic(fadeOutTime));

            yield return fadeInMusicCoroutine = StartCoroutine(FadeInMusic(musicTrack, fadeInTime));
        }

        yield return null;
    }

   
    private IEnumerator FadeOutMusic(float fadeOutTime)
    {
        HelperUtilities.GameResources.musicLowSnapshot.TransitionTo(fadeOutTime);

        yield return new WaitForSeconds(fadeOutTime);
    }

    private IEnumerator FadeInMusic(MusicTrackSO musicTrack,float fadeInTime)
    {
        audioSource.clip = musicTrack.MusicClip;
        audioSource.volume = musicTrack.MusicVolume;
        audioSource.Play();

        HelperUtilities.GameResources.musicOnFullSnapshot.TransitionTo(fadeInTime);
        yield return new WaitForSeconds(fadeInTime);
    }
     

}
