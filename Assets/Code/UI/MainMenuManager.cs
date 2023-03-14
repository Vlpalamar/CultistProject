using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    #region Header UI
    [Space(5)]
    [Header("UI")]
    #endregion
    [SerializeField] private MusicTrackSO mainMenuTheme;
    [SerializeField] private Slider _soundVolume;
    [SerializeField] private Slider _musicVolume;

    #region Variables
    [Space(5)]
    [Header("Variables")]
    #endregion
    [SerializeField] private string nextSceneName;

    private const string soundValueKey = "soundValueKey";
    private const string musicValueKey = "musicValueKey";


    private void Start()
    {
        MusicManager.Instance.PlayMusic(mainMenuTheme,0.2f, 2f);
        Initialise();
        ChangeSoundVolume();
        ChangeMusicVolume();
    }

    private void Initialise()
    {
        if (PlayerPrefs.HasKey(soundValueKey))
            _soundVolume.value = PlayerPrefs.GetInt(soundValueKey);

        if (PlayerPrefs.HasKey(musicValueKey))
            _musicVolume.value = PlayerPrefs.GetInt(musicValueKey);
    }

    public void ChangeSoundVolume()
    {
        SoundEffectManager.Instance.ChangeSoundsVolume((int)_soundVolume.value);

        PlayerPrefs.SetInt(soundValueKey,(int) _soundVolume.value);
    }

    public void ChangeMusicVolume()
    {
        MusicManager.Instance.ChangeSoundsVolume((int)_musicVolume.value);

        PlayerPrefs.SetInt(musicValueKey, (int)_musicVolume.value);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(nextSceneName);
    }
    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(nextSceneName);
    }

}
