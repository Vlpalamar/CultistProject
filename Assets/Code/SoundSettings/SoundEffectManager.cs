using System;
using System.Collections;
using UnityEngine;



[DisallowMultipleComponent]
public class SoundEffectManager : SingletonMonoBehaviour<SoundEffectManager>
{


    public int soundVolume = 8;

    

    private void Start()
    {
        SetSoundsVolume(soundVolume);
       
    }

    private void SetSoundsVolume(int soundVolume)
    {
        float muteDecibels = -80f;

        if (soundVolume ==0)
            HelperUtilities.GameResources.audioMasterMixerGroup.audioMixer.SetFloat("soundsVolume", muteDecibels);
        else
            HelperUtilities.GameResources.audioMasterMixerGroup.audioMixer.SetFloat("soundsVolume", HelperUtilities.LinearToDecibels(soundVolume));
        
    }

    public void PlaySoundEffect(SoundEffectSO soundEffect)
    {
        SoundEffect sound = (SoundEffect)PoolManager.Instance.ReuseComponent(soundEffect.SoundPrefab, Vector3.zero, Quaternion.identity);
        sound.SetSound(soundEffect);
        sound.gameObject.SetActive(true);
        StartCoroutine(DisableSound(sound, soundEffect.AudioClip.length));
    }

    private IEnumerator DisableSound(SoundEffect sound, float length)
    {
        yield return new WaitForSeconds(length);
        sound.gameObject.SetActive(false);
    }
}
