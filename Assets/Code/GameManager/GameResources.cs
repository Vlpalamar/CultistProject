using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "GameResources_", menuName = "Scriptable Objects/GameResources")]
public  class GameResources : ScriptableObject
{
    #region PLAYER
    [Space(10)]
    [Header("Player")]
    #endregion
    #region
    [Tooltip("This is used for reference between sceens")]
    #endregion
    public PlayerDetailsSO currentPlayerDetails;


    #region  Sounds
    [Space(10)]
    [Header("Sounds")]
    #endregion
    public AudioMixerGroup audioMasterMixerGroup;

    #region Header Music
    [Space(10)]
    [Header("Music")]

    #endregion
    #region Tooltip
    [Tooltip("Populate with the music master mixer group")]
    #endregion
    public AudioMixerGroup musicMixerGroup;
    #region Tooltip
    [Tooltip("Music on full snapshot")]
    #endregion
    public AudioMixerSnapshot musicOnFullSnapshot;
    #region Tooltip
    [Tooltip("Music low snapshot")]
    #endregion
    public AudioMixerSnapshot musicLowSnapshot;
    #region Tooltip
    [Tooltip("Music off snapshot")]
    #endregion
    public AudioMixerSnapshot musicOffSnapshot;



}

